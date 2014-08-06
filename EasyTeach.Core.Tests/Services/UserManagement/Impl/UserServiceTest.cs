using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Repositories.Mappers.UserManagement;
using EasyTeach.Core.Services.Messaging;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Core.Validation.EntityValidator;

using FakeItEasy;
using Microsoft.AspNet.Identity;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class UserServiceTest
    {
        private class User : IUserModel
        {
            public int UserId { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public IGroupModel Group { get; set; }

            [Required]
            [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Not valid Email address")]
            public string Email { get; set; }

            [EnumDataType(typeof(UserType))]
            public UserType UserType { get; set; }
        }

        private readonly UserManager<IUserDto, int> _userManager;
        private readonly IUserService _userService;
        private readonly IUserDtoMapper _userDtoMapper;
        private readonly IEmailService _emailService;
        private readonly IUserModel _validUser;
        private readonly EntityValidator _entityValidator;
        private readonly IUserModel _invalidEmptyUser;
        private readonly IUserRepository _userRepository;

        public UserServiceTest()
        {
            _userManager = A.Fake<UserManager<IUserDto, int>>();
            _userDtoMapper = A.Fake<IUserDtoMapper>();
            _emailService = A.Fake<IEmailService>();
            _entityValidator = A.Fake<EntityValidator>();
            _userRepository = A.Fake<IUserRepository>();


            _userService = new UserService(_userManager,
                _userDtoMapper, 
                _emailService, 
                _entityValidator,
                _userRepository, 
                o => new ValidationContext(o, null, null));
            _validUser = new User
            {
                FirstName = "test",
                LastName = "test",
                UserType = UserType.Student,
                Group = new Group { GroupNumber = 2, Year = 2009 },
                Email = "test@test.com"
            };
            _invalidEmptyUser = new User();
        }

        [Fact]
        public void CreateUserAsync_ValidStudentUser_UserCreated()
        {
            var userDto = A.Fake<IUserDto>();

            A.CallTo(() => _entityValidator.ValidateEntity(_validUser, A<ValidationContext>.Ignored)).Returns(new EntityValidationResult(true));
            A.CallTo(() => _userDtoMapper.Map(_validUser)).Returns(userDto);
            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored)).Returns((IUserDto)null);
            A.CallTo(() => _userManager.CreateAsync(userDto)).Returns(IdentityResult.Success);

            Assert.DoesNotThrow(() => _userService.CreateUserAsync(_validUser).Wait());
            A.CallTo(() => _userManager.CreateAsync(userDto)).MustHaveHappened();
            A.CallTo(() => _userDtoMapper.Map(_validUser)).MustHaveHappened();
            A.CallTo(() => _emailService.SendUserRegistrationConfirmationEmailAsync(A<IUserDto>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void CreateUserAsync_InvalidUser_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => _userDtoMapper.Map(_invalidEmptyUser)).Returns(userDto);
            A.CallTo(() => _entityValidator.ValidateEntity(_invalidEmptyUser, A<ValidationContext>.Ignored)).Returns(new EntityValidationResult(false, new List<ValidationResult> {
                                                          new ValidationResult("error", new[] { "FirstName" }), 
                                                          new ValidationResult("error", new[] { "LastName" }), 
                                                          new ValidationResult("error", new[] { "Email" }), 
                                                          new ValidationResult("error", new[] { "Group" }), 
                                                          new ValidationResult("error", new[] { "UserType" }),
                                                      }));

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.CreateUserAsync(_invalidEmptyUser).Wait());
            var exception = (InvalidUserDataException) aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "FirstName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "LastName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Email"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "UserType"));

            A.CallTo(() => _userManager.CreateAsync(A<IUserDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _userDtoMapper.Map(A<IUserModel>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _emailService.SendUserRegistrationConfirmationEmailAsync(A<IUserDto>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void FindUserByCredentialsAsync_ValidCredentails_ReturnUserIdentity()
        {
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => userDto.Email).Returns("test@example.com");
            A.CallTo(() => userDto.UserId).Returns(42);
            A.CallTo(() => _userManager.FindAsync("test@example.com", "test")).Returns(userDto);

            IUserIdentityModel userIdentity = _userService.FindUserByCredentialsAsync("test@example.com", "test").Result;
            Assert.Equal("test@example.com", userIdentity.Email);
            Assert.Equal(42, userIdentity.UserId);
        }

        [Fact]
        public void FindUserByCredentialsAsync_InvalidCredentails_ReturnNull()
        {
            A.CallTo(() => _userManager.FindAsync("test@example.com", "test")).Returns(Task.FromResult<IUserDto>(null));

            IUserIdentityModel userIdentity = _userService.FindUserByCredentialsAsync("test@example.com", "test").Result;
            Assert.Null(userIdentity);
        }

        [Fact]
        public void ConfirmUserEmailAsync_InvalidToken_InvalidEmailConfirmationOperationExceptionThrown()
        {
            A.CallTo(() => _userManager.ConfirmEmailAsync(A<int>.Ignored, A<string>.Ignored)).Returns(new IdentityResult());

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, "SomeToken").Wait());

            Assert.IsAssignableFrom<InvalidEmailConfirmationOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void ConfirmUserEmailAsync_NotFoundUser_InvalidEmailConfirmationOperationExceptionThrown()
        {
            A.CallTo(() => _userManager.ConfirmEmailAsync(A<int>.Ignored, A<string>.Ignored)).Throws(new AggregateException(new InvalidOperationException()));

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.ConfirmUserEmailAsync(A<int>.Ignored, "SomeToken").Wait());

            Assert.IsAssignableFrom<InvalidEmailConfirmationOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void ConfirmUserEmailAsync_ExistingUserAndValidToken_GeneratePasswordResetTokenAsyncCalled()
        {
            A.CallTo(() => _userManager.ConfirmEmailAsync(A<int>.Ignored, A<string>.Ignored)).Returns(IdentityResult.Success);

            _userService.ConfirmUserEmailAsync(A<int>.Ignored, "SomeToken").Wait();

            A.CallTo(() => _userManager.GeneratePasswordResetTokenAsync(A<int>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void SetUserPasswordAsync_InvalidToken_InvalidSetPasswordOperationExceptionThrown()
        {
            A.CallTo(() => _userManager.ResetPasswordAsync(A<int>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(new IdentityResult());

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.SetUserPasswordAsync(A<int>.Ignored, "SomeToken", "SomePassword").Wait());

            Assert.IsAssignableFrom<InvalidSetPasswordOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void SetUserPasswordAsync_NotFoundUser_InvalidSetPasswordOperationExceptionThrown()
        {
            A.CallTo(() => _userManager.ResetPasswordAsync(A<int>.Ignored, A<string>.Ignored, A<string>.Ignored)).Throws(new AggregateException(new InvalidOperationException()));

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.SetUserPasswordAsync(A<int>.Ignored, "SomeToken", "SomePassword").Wait());

            Assert.IsAssignableFrom<InvalidSetPasswordOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void SetUserPasswordAsync_ExistingUserAndValidToken_NotThrownException()
        {
            A.CallTo(() => _userManager.ResetPasswordAsync(A<int>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(IdentityResult.Success);

            Assert.DoesNotThrow(() => _userService.SetUserPasswordAsync(A<int>.Ignored, "SomeToken", "SomePassword").Wait());
        }

        [Fact]
        public void ResetUserPasswordAsync_UserEmailNotValidated_InvalidResetPasswordOperationExceptionThrown()
        {
            var user = A.Fake<IUserDto>();
            A.CallTo(() => user.EmailIsValidated).Returns(false);

            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored)).Returns(user);

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.ResetUserPasswordAsync("test@example.org").Wait());

            Assert.IsAssignableFrom<InvalidResetPasswordOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void ResetUserPasswordAsync_NotFoundUser_InvalidResetPasswordOperationExceptionThrown()
        {
            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored)).Returns((IUserDto)null);

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.ResetUserPasswordAsync("test@example.org").Wait());

            Assert.IsAssignableFrom<InvalidResetPasswordOperationException>(aggregateException.GetBaseException());
        }

        [Fact]
        public void ResetUserPasswordAsync_ExtisngUserWithValidedEmail_SendResetUserPasswordEmailAsyncCalled()
        {
            var user = A.Fake<IUserDto>();
            A.CallTo(() => user.EmailIsValidated).Returns(true);

            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored)).Returns(user);

            _userService.ResetUserPasswordAsync("test@example.com").Wait();

            A.CallTo(() => _emailService.SendResetUserPasswordEmailAsync(user)).MustHaveHappened();
        }
    }
}
