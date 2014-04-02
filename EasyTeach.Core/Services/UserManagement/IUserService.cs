using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;

namespace EasyTeach.Core.Services.UserManagement
{
    public interface IUserService
    {
        IUserRepository UserRepository { get; set; }

        void CreateUser(User newUser);
    }
}
