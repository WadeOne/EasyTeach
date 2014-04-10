using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Email;

namespace EasyTeach.Web.Services.Email.Impl
{
    public sealed class EmailBuilder : IEmailBuilder
    {
        public Core.Services.Email.Email BuildRegsitrationConfirmationEmail(IUserDto user, string token)
        {
            return new Core.Services.Email.Email
            {
                Body = token,
                Subject = "Confirmation EasyTeach registration"
            };
        }
    }
}