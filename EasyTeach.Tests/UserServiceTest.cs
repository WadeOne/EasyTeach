using System;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Exceptions;
using EasyTeach.Core.Interfaces.Repositories;
using EasyTeach.Core.Interfaces.Services;
using EasyTeach.Domain.Services;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public void CreateUser_ValidStudentUser_UserCreated()
        {
            //Arrange
            var userRepo = A.Fake<IUserRepository>();
            IUserService userService = new UserService();
            userService.UserRepository = userRepo;
            User user = new User();
            user.FirstName = "test";
            user.LastName = "test";
            user.Group = new Group { GroupNumber = 2, Year = 2009 };
            user.Email = "test@test.com";

            //Act/Assert
            Assert.DoesNotThrow(() => userService.CreateUser(user));
            A.CallTo(() => userRepo.SaveUser(user)).MustHaveHappened();
        }

        [Fact]
        public void CreateUser_InvalidUser_InvalidUserExceptionThrownUserNotCreatedProperError()
        {
            //Arrange
            var userRepo = A.Fake<IUserRepository>();
            IUserService userService = new UserService();
            userService.UserRepository = userRepo;
            User user = new User();

            //Act/ASsert
            var exception = Assert.Throws<InvalidUserException>(() => userService.CreateUser(user));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "FirstName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "LastName"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Email"));
            Assert.True(exception.ValidationResults.Any(x => x.MemberNames.First() == "Group"));
            A.CallTo(() => userRepo.SaveUser(user)).MustNotHaveHappened();
        }
    }
}
