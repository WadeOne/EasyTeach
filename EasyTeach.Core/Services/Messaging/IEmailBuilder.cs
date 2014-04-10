using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Services.Messaging
{
    public interface IEmailBuilder
    {
        Email BuildRegsitrationConfirmationEmail(IUserDto user, string token);
    }
}