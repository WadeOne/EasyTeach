using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.UserManagement
{
    public interface IUserService
    {
        void CreateUser(IUserModel newUser);
    }
}
