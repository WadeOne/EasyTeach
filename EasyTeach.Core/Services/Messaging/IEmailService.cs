using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Services.Messaging
{
    public interface IEmailService
    {
        Task SendUserRegistrationConfirmationEmailAsync(IUserDto user);
    }
}