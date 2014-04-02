using EasyTeach.Core.Entities;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        void SaveUser(User newUser);
    }
}