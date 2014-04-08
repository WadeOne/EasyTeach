using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.UserManagement
{
    public interface IUserService
    {
        Task CreateUserAsync(IUserModel newUser);

        Task<IUserModel> Login(string login, string password);
    }
}
