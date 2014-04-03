using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository
    {
        IUserDto GetUserByEmail(string email);

        void SaveUser(IUserDto newUser);
    }
}