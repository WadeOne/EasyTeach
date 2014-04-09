using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Enums;

namespace EasyTeach.Web.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Group Group { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual User ToUser()
        {
            return new User
                   {
                       Email = Email,
                       FirstName = FirstName,
                       Group = Group,
                       LastName = LastName,
                       UserType = UserType.Student
                   };
        }

    }
}