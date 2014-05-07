using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Entities.Data.User;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Data.Entities
{
    public sealed class UserDto : IUserDto
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        IGroupDto IUserDto.Group
        {
            get { return Group; }
        }

        public GroupDto Group { get; set; }

        public string Email { get; set; }

        public bool EmailIsValidated { get; set; }

        public string PasswordHash { get; set; }

        int IUser<int>.Id
        {
            get { return UserId; }
        }

        string IUser<int>.UserName
        {
            get { return Email; }
            set { Email = value; }
        }
    }
}
