using EasyTeach.Core.Enums;

using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Entities.Data.User
{
    public interface IUserDto : IUser<int>
    {
        int UserId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        Group Group { get; set; }

        string Email { get; set; }

        bool EmailIsValidated { get; set; }

        UserType UserType { get; set; }

        string PasswordHash { get; set; }
    }
}
