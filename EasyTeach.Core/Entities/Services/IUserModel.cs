using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IUserModel
    {
        int UserId { get; }

        string FirstName { get; }

        string LastName { get; }

        Group Group { get; }

        string Email { get; }

        bool EmailIsValidated { get; }

        UserType UserType { get; }
    }
}
