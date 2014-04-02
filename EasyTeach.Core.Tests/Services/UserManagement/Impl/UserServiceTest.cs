using System.Linq;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Core.Tests.Services.UserManagement.Impl
{
    public sealed class UserServiceTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void CreateUser_ValidStudentUser_UserCreated()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                UserType = UserType.Student,
                Group = new Group { GroupNumber = 2, Year = 2009 },
                Email = "test@test.com"
            };

            A.CallTo(() => _userRepository.GetUserByEmail(A<string>.Ignored)).Returns(null);

            Assert.DoesNotThrow(() => _userService.CreateUser(user));
            A.CallTo(() => _userRepository.SaveUser(user)).MustHaveHappened();
        }

        [Fact]
        public void CreateUser_InvalidUser_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var user = new User();

            var exception = Assert.Throws<InvalidUserDataException>(() => _userService.CreateUser(user));

            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "FirstName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "LastName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Email"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "UserType"));

            A.CallTo(() => _userRepository.SaveUser(user)).MustNotHaveHappened();
        }

        [Fact]
        public void CreateUser_UserWithDuplicateEmail_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                UserType = UserType.Student,
                Group = new Group { GroupNumber = 2, Year = 2009 },
                Email = "test@test.com"
            };

            A.CallTo(() => _userRepository.GetUserByEmail("test@test.com")).Returns(new User());

            var exception = Assert.Throws<InvalidUserDataException>(() => _userService.CreateUser(user));

            Assert.True(exception.ValidationResults.All(x => x.MemberNames.First() == "Email"));
            A.CallTo(() => _userRepository.SaveUser(user)).MustNotHaveHappened();
        }
    }
}
