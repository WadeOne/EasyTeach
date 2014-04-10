using System;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;

namespace EasyTeach.Web.Services.Email.Impl
{
    public sealed class EmailBuilder : IEmailBuilder
    {
        public Core.Services.Messaging.Email BuildRegsitrationConfirmationEmail(IUserDto user, string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("token");
            }

            return new Core.Services.Messaging.Email
            {
                Body = token,
                Subject = "Confirmation EasyTeach registration"
            };
        }
    }
}