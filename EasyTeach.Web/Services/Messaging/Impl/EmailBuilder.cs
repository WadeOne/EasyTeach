using System;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;

namespace EasyTeach.Web.Services.Messaging.Impl
{
    public sealed class EmailBuilder : IEmailBuilder
    {
        public Email BuildRegsitrationConfirmationEmail(IUserDto user, string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("token");
            }

            return new Email
            {
                Body = token,
                Subject = "Confirmation EasyTeach registration"
            };
        }
    }
}