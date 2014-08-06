using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;
using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Models.ViewModels.UserManagement;

using Xunit;

namespace EasyTeach.Web.Tests.Models.ViewModels
{
    public class CreateUserViewModelTest
    {
        [Fact]
        public void ToUser_CreateUserViewModel_CorrectlyMapped()
        {
            const string FirstName = "FirstName";
            const string LastName = "LastName";
            const string Email = "Email";
            Group Group = new Group { GroupNumber = 2, Year = 2009 };

            var user = new UserViewModel
                       {
                           FirstName = FirstName,
                           Email = Email,
                           LastName = LastName,
                           Group = Group
                       };

            var userModel = user.ToUser();

            Assert.NotNull(userModel);
            Assert.Equal(FirstName, userModel.FirstName);
            Assert.Equal(LastName, userModel.LastName);
            Assert.Equal(Email, userModel.Email);
            Assert.Equal(Group.GroupNumber, userModel.Group.GroupNumber);
            Assert.Equal(Group.Year, userModel.Group.Year);
            Assert.Equal(default(int), userModel.UserId);
        }
    }
}
