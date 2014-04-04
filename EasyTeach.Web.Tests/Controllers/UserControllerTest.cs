using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Core.Services.UserManagement;
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

        //[Fact]
        //public void Post_InvalidUser_UserNotCreatedErrorResultSent()
        //{
        //    var user = new User();


        //}
    }
}
