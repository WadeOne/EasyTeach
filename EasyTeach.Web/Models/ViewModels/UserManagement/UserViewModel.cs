using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Web.Models.ViewModels.Groups;

namespace EasyTeach.Web.Models.ViewModels.UserManagement
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public int? GroupId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual User ToUser()
        {
            return new User
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Group = GroupId.HasValue ? new Group
                {
                    GroupId = GroupId.Value
                } : null
            };
}
    }
}