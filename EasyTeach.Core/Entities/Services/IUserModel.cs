using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IUserModel : IUserIdentityModel
    {
        string FirstName { get; }

        string LastName { get; }

        Group Group { get; }

        UserType UserType { get; }
    }
}
