using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Controllers;
using EasyTeach.Web.Models;
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
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@test.com",
                Group = new Group
                {
                    GroupNumber = 2,
                    Year = 2009
                },
                UserType = UserType.Student
            };

            var result = _userController.Post(user);
            A.CallTo(() => _userService.CreateUser(user)).MustHaveHappened();
            Assert.True(result.Success);
            Assert.Null(result.Errors);
            Assert.True(result.Message.Equals("User have been successfully created"));
        }

        [Fact]
        public void Post_InvalidUser_UserNotCreatedErrorResultSent()
        {
            var user = new User();
            A.CallTo(() => _userService.CreateUser(user))
                .Throws(() => new InvalidUserDataException(new Collection<ValidationResult>
                                                           {
                                                               new ValidationResult("FirstName is required", new List<string> {"FirstName"}),
                                                               new ValidationResult("LastName is required", new List<string> {"LastName"}),
                                                               new ValidationResult("Group is required", new List<string> {"Group"}),
                                                               new ValidationResult("Email is required", new List<string> {"Email"}),
                                                               new ValidationResult("Wrong UserType", new List<string> {"UserType"}),
                                                           }));

            var result = _userController.Post(user);

            A.CallTo(() => _userService.CreateUser(user)).MustHaveHappened();
            Assert.False(result.Success);
            Assert.True(result.Message.Equals("Some fields are not correct"));
            Assert.NotNull(result.Errors);
            Assert.True(result.Errors.Any(ei => ei.PropertyName == "FirstName"));
            Assert.True(result.Errors.Any(ei => ei.PropertyName == "LastName"));
            Assert.True(result.Errors.Any(ei => ei.PropertyName == "Group"));
            Assert.True(result.Errors.Any(ei => ei.PropertyName == "Email"));
            Assert.True(result.Errors.Any(ei => ei.PropertyName == "UserType"));
        }
    }
}
