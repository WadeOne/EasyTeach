using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Core.Services.Email
{
    public interface IEmailBuilder
    {
        Email BuildRegsitrationConfirmationEmail(IUserDto user, string token);
    }
}