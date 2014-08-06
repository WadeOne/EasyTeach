using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.UserManagement
{
    public interface IUserService
    {
        Task CreateUserAsync(IUserModel newUser);

        Task<IUserIdentityModel> FindUserByCredentialsAsync(string email, string password);

        Task<ClaimsIdentity> CreateUserIdentityClaimsAsync(IUserIdentityModel userIdentity, string authenicationType);

        Task<string> ConfirmUserEmailAsync(int userId, string token);

        Task SetUserPasswordAsync(int userId, string resetPasswordToken, string password);

        Task ResetUserPasswordAsync(string email);

        IQueryable<IUserModel> GetUsers();
    }
}
