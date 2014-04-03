using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Data
{
    public interface IUserDto
    {
        int UserId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        Group Group { get; set; }

        string Email { get; set; }

        bool EmailIsValidated { get; set; }

        UserType UserType { get; set; }
    }
}
