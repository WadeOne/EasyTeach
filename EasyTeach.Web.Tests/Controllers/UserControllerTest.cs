using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Controllers;
using EasyTeach.Web.Models.ViewModels;

using FakeItEasy;
using Microsoft.Owin.Security;
using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public sealed class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _authenticationManager = A.Fake<IAuthenticationManager>();
            _userController = new UserController(_userService, () => _authenticationManager);
        }

        [Fact]
        public void Register_ValidUser_UserCreatedAndOkResultSent()
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

            var result = _userController.Register(user).Result as OkResult;
            A.CallTo(() => _userService.CreateUserAsync(userModel)).MustHaveHappened();
            Assert.NotNull(result);
        }

        [Fact]
        public void Register_InvalidUser_UserNotCreatedErrorResultSent()
        {
            var userModel = new User();
            var user = A.Fake<CreateUserViewModel>();

            A.CallTo(() => user.ToUser()).Returns(userModel);
            A.CallTo(() => _userService.CreateUserAsync(userModel))
                .Throws(() => new InvalidUserDataException(new Collection<ValidationResult>
                                                           {
                                                               new ValidationResult("FirstName is required", new List<string> {"FirstName"}),
                                                               new ValidationResult("LastName is required", new List<string> {"LastName"}),
                                                               new ValidationResult("Email is required", new List<string> {"Email"}),
                                                               new ValidationResult("Wrong UserType", new List<string> {"UserType"}),
                                                           }));

            var result = _userController.Register(user).Result as InvalidModelStateResult;

            A.CallTo(() => _userService.CreateUserAsync(userModel)).MustHaveHappened();
            Assert.NotNull(result);
            Assert.True(result.ModelState.Any(ei => ei.Key == "FirstName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "LastName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "Email"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "UserType"));
        }

        [Fact]
        public void Logout_SignOutMustHaveHappened()
        {
            _userController.Logout();
            A.CallTo(() => _authenticationManager.SignOut("Cookies")).MustHaveHappened();
        }

        [Fact]
        public void Logout_OkResultSent()
        {
            Assert.IsAssignableFrom<OkResult>(_userController.Logout());
        }
    }
}
