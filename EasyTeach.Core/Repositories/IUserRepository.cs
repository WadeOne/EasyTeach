using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IUserDto> GetUserByEmail(string email);

        Task<IUserDto> GetUserById(int userId);

        Task CreateAsync(IUserDto user);

        Task UpdateAsync(IUserDto user);
    }
}