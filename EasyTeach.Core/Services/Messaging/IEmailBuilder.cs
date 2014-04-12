using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Services.Messaging
{
    public interface IEmailBuilder
    {
        Task<Email> BuildRegsitrationConfirmationEmailAsync(IUserDto user, string token);
    }
}