using EasyTeach.Core.Entities;

namespace EasyTeach.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(User newUser);
    }
}