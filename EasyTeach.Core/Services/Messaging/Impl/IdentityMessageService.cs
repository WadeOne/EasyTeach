using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.Messaging.Impl
{
    public sealed class IdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.SendMailAsync(
                    new MailMessage(
                        new MailAddress("easyteach@example.com"),
                        new MailAddress(message.Destination))
                    {
                        Body = message.Body,
                        Subject = message.Subject
                    });
            }
        }
    }
}