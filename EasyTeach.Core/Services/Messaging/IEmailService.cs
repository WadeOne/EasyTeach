using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Services.Messaging
{
    public interface IEmailService
    {
        Task SendUserRegistrationConfirmationEmailAsync(IUserDto user);

        Task SendResetUserPasswordEmailAsync(IUserDto user);
    }
}