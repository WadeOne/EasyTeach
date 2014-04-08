using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IUserDto> GetUserByEmail(string email);

        Task CreateAsync(IUserDto user);

        Task UpdateAsync(IUserDto user);
    }
}