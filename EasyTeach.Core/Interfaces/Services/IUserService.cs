using EasyTeach.Core.Entities;
using EasyTeach.Core.Interfaces.Repositories;

namespace EasyTeach.Core.Interfaces.Services
{
    public interface IUserService
    {
        IUserRepository UserRepository { get; set; }

        void CreateUser(User newUser);
    }
}
