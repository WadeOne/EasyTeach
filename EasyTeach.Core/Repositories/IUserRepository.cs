using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Repositories
{
    public interface IUserRepository : IUserStore<IUserDto, int>
    {
        Task<IUserDto> GetUserByEmail(string email);
    }
}