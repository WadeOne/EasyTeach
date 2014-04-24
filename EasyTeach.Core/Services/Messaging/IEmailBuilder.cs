using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Data.User;

namespace EasyTeach.Core.Services.Messaging
{
    public interface IEmailBuilder
    {
        Task<Email> BuildRegsitrationConfirmationEmailAsync(IUserDto user, string confirmEmailToken);

        Task<Email> BuildResetPasswordEmailAsync(IUserDto user, string resetPasswordToken);
    }
}