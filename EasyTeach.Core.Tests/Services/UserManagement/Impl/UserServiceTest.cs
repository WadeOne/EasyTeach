using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;

using FakeItEasy;
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

            public bool EmailIsValidated { get; set; }

            [EnumDataType(typeof(UserType))]
            public UserType UserType { get; set; }
        }

        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUserDtoMapper _userDtoMapper;

        private readonly IUserModel _validUser;

        public UserServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userDtoMapper = A.Fake<IUserDtoMapper>();
            _userService = new UserService(_userRepository, _userDtoMapper);

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
        public void CreateUser_ValidStudentUser_UserCreated()
        {
            var userDto = A.Fake<IUserDto>();

            A.CallTo(() => _userDtoMapper.Map(_validUser)).Returns(userDto);
            A.CallTo(() => _userRepository.GetUserByEmail(A<string>.Ignored)).Returns(null);

            Assert.DoesNotThrow(() => _userService.CreateUser(_validUser));
            A.CallTo(() => _userRepository.SaveUser(userDto)).MustHaveHappened();
            A.CallTo(() => _userDtoMapper.Map(_validUser)).MustHaveHappened();
        }

        [Fact]
        public void CreateUser_InvalidUser_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var user = new User();
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => _userDtoMapper.Map(user)).Returns(userDto);

            var exception = Assert.Throws<InvalidUserDataException>(() => _userService.CreateUser(user));

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "FirstName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "LastName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Email"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "UserType"));

            A.CallTo(() => _userRepository.SaveUser(A<IUserDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _userDtoMapper.Map(A<IUserModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void CreateUser_UserWithDuplicateEmail_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var userDto = A.Fake<IUserDto>();
            A.CallTo(() => _userDtoMapper.Map(_validUser)).Returns(userDto);

            A.CallTo(() => _userRepository.GetUserByEmail("test@test.com")).Returns(userDto);

            var exception = Assert.Throws<InvalidUserDataException>(() => _userService.CreateUser(_validUser));

            Assert.True(exception.ValidationResults.All(x => x.MemberNames.First() == "Email"));
            A.CallTo(() => _userRepository.SaveUser(A<IUserDto>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _userDtoMapper.Map(A<IUserModel>.Ignored)).MustNotHaveHappened();
        }
    }
}
