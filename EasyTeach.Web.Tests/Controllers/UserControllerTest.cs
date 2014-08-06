using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Controllers;
using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Models.ViewModels.UserManagement;

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
            var user = A.Fake<UserViewModel>();
            var userModel = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@test.com",
                Group = new Group { GroupNumber = 2, Year = 2009 },
            };

            A.CallTo(() => user.ToUser()).Returns(userModel);

            OkResult result = ActWithSufficientClaims(() => _userController.Register(user).Result as OkResult);
            A.CallTo(() => _userService.CreateUserAsync(userModel)).MustHaveHappened();
            Assert.NotNull(result);
        }

        [Fact]
        public void Register_InvalidUser_UserNotCreatedErrorResultSent()
        {
            var userModel = new User();
            var user = A.Fake<UserViewModel>();

            A.CallTo(() => user.ToUser()).Returns(userModel);
            A.CallTo(() => _userService.CreateUserAsync(userModel))
                .Throws(() => new InvalidUserDataException(new Collection<ValidationResult>
                                                           {
                                                               new ValidationResult("FirstName is required", new List<string> {"FirstName"}),
                                                               new ValidationResult("LastName is required", new List<string> {"LastName"}),
                                                               new ValidationResult("Email is required", new List<string> {"Email"}),
                                                               new ValidationResult("Wrong UserType", new List<string> {"UserType"}),
                                                           }));

            var result = ActWithSufficientClaims(() => _userController.Register(user).Result as InvalidModelStateResult);

            A.CallTo(() => _userService.CreateUserAsync(userModel)).MustHaveHappened();
            Assert.NotNull(result);
            Assert.True(result.ModelState.Any(ei => ei.Key == "FirstName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "LastName"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "Email"));
            Assert.True(result.ModelState.Any(ei => ei.Key == "UserType"));
        }

        [Fact]
        public void ConfirmEmail_ValidUserAndToken_OkResultWithResetPasswordToken()
        {
            A.CallTo(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult("qwe34556"));

            var result = Assert.IsAssignableFrom<OkNegotiatedContentResult<ResetPasswordTokenViewModel>>(_userController.ConfirmEmail(new ConfirmActionViewModel
            {
                ConfirmEmailToken = "test",
                UserId = 42
            }).Result);

            Assert.Equal("qwe34556", result.Content.ResetPasswordToken);
        }

        [Fact]
        public void ConfirmEmail_InvalidModel_InvalidModelStateResult()
        {
            _userController.ModelState.AddModelError("token", "invalid");

            Assert.IsAssignableFrom<InvalidModelStateResult>(_userController.ConfirmEmail(new ConfirmActionViewModel
            {
                ConfirmEmailToken = null,
                UserId = 42
            }).Result);

            A.CallTo(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, A<string>.Ignored))
                .MustNotHaveHappened();
        }

        [Fact]
        public void ConfirmEmail_ConfirmUserEmailAsyncThrowsInvalidEmailConfirmationOperationException_InvalidModelStateResult()
        {
            var exception = new InvalidEmailConfirmationOperationException(new ValidationResult(" "));

            A.CallTo(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, A<string>.Ignored))
                .Throws(exception);

            Assert.IsAssignableFrom<InvalidModelStateResult>(_userController.ConfirmEmail(new ConfirmActionViewModel
            {
                ConfirmEmailToken = "test",
                UserId = 42
            }).Result);

            A.CallTo(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, A<string>.Ignored))
                .MustHaveHappened();
        }

        [Fact]
        public void ResetPassword_ValidUserEmail_OkResult()
        {
            A.CallTo(() => _userService.ResetUserPasswordAsync( A<string>.Ignored))
                .Returns(Task.FromResult(0));

            Assert.IsAssignableFrom<OkResult>(_userController.ResetPassword("test@example.com").Result);

            A.CallTo(() => _userService.ResetUserPasswordAsync(A<string>.Ignored))
                .MustHaveHappened();
        }

        [Fact]
        public void ResetPassword_EmptyEmail_InvalidModelStateResult()
        {
            Assert.IsAssignableFrom<InvalidModelStateResult>(_userController.ResetPassword("").Result);

            A.CallTo(() => _userService.ResetUserPasswordAsync(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void ResetPassword_ResetUserPasswordAsyncThrowsInvalidResetPasswordOperationException_InvalidModelStateResult()
        {
            var exception = new InvalidResetPasswordOperationException(new ValidationResult(" "));

            A.CallTo(() => _userService.ResetUserPasswordAsync(A<string>.Ignored))
                .Throws(exception);

            Assert.IsAssignableFrom<InvalidModelStateResult>(_userController.ResetPassword("test@example.com").Result);

            A.CallTo(() => _userService.ResetUserPasswordAsync(A<string>.Ignored))
                .MustHaveHappened();
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

        private static T ActWithSufficientClaims<T>(Func<T> action)
        {
            var oldPrincipal = Thread.CurrentPrincipal;

            try
            {
                Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("User", "Register") }));
                return action();
            }
            finally
            {
                Thread.CurrentPrincipal = oldPrincipal;
            }
        }
    }
}
