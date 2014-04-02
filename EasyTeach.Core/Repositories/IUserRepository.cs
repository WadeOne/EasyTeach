using EasyTeach.Core.Entities;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(User newUser);
    }
}