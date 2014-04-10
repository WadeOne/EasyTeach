using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Messaging;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;

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
            public Group Group { get; set; }

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

        public UserServiceTest()
        {
            _userManager = A.Fake<UserManager<IUserDto, int>>();
            _userDtoMapper = A.Fake<IUserDtoMapper>();
            _emailService = A.Fake<IEmailService>();
            _userService = new UserService(_userManager, _userDtoMapper, _emailService);

            _validUser = new User
            {
                FirstName = "test",
                LastName = "test",
                UserType = UserType.Student,
                Group = new Group { GroupNumber = 2, Year = 2009 },
                Email = "test@test.com"
            };
        }

        [Fact]
        public void CreateUserAsync_ValidStudentUser_UserCreated()
        {
            var userDto = A.Fake<IUserDto>();

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
            var user = new User();
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => _userDtoMapper.Map(user)).Returns(userDto);

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.CreateUserAsync(user).Wait());
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
        public void CreateUserAsync_UserWithDuplicateEmail_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => _userDtoMapper.Map(_validUser)).Returns(userDto);

            A.CallTo(() => _userManager.FindByEmailAsync("test@test.com")).Returns(userDto);

            var aggregateException = Assert.Throws<AggregateException>(() => _userService.CreateUserAsync(_validUser).Wait());
            var exception = (InvalidUserDataException)aggregateException.GetBaseException();

            Assert.True(exception.ValidationResults.All(x => x.MemberNames.First() == "Email"));
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
    }
}
