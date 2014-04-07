using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Controllers;
using EasyTeach.Web.Models;
using EasyTeach.Web.Models.ViewModels;

using FakeItEasy;
using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService _userService; 
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _userController = new UserController(_userService);
        }

        [Fact]
        public void Post_ValidUser_UserCreatedAndOkResultSent()
        {
            var user = A.Fake<CreateUserViewModel>();
            var userModel = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@test.com",
                Group = new Group { GroupNumber = 2, Year = 2009 },
                EmailIsValidated = false
            };

            A.CallTo(() => user.ToUser()).Returns(userModel);

            var result = _userController.Post(user) as OkResult;
            A.CallTo(() => _userService.CreateUser(userModel)).MustHaveHappened();
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_InvalidUser_UserNotCreatedErrorResultSent()
        {
            var userModel = new User();
            var user = A.Fake<CreateUserViewModel>();

            A.CallTo(() => user.ToUser()).Returns(userModel);
            A.CallTo(() => _userService.CreateUser(userModel))
                .Throws(() => new InvalidUserDataException(new Collection<ValidationResult>
                                                           {
                                                               new ValidationResult("FirstName is required", new List<string> {"FirstName"}),
                                                               new ValidationResult("LastName is required", new List<string> {"LastName"}),
                                                               new ValidationResult("Email is required", new List<string> {"Email"}),
                                                               new ValidationResult("Wrong UserType", new List<string> {"UserType"}),
                                                           }));

            var result = _userController.Post(user) as InvalidModelStateResult;

            A.CallTo(() => _userService.CreateUser(userModel)).MustHaveHappened();
            Assert.NotNull(result);
            Assert.True(result.ModelState.Any(ei => ei.Key == "FirstName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "LastName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "Email"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "UserType"));
        }
    }
}
