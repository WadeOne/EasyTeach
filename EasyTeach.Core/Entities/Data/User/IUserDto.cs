using EasyTeach.Core.Entities.Data.Group;

using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Entities.Data.User
{
    public interface IUserDto : IUser<int>
    {
        int UserId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        IGroupDto Group { get; }

        string Email { get; set; }

        bool EmailIsValidated { get; set; }

        string PasswordHash { get; set; }
    }
}
