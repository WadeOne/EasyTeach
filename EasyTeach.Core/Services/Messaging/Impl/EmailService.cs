using System;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.Messaging.Impl
{
    public sealed class EmailService : IEmailService
    {
        private readonly UserManager<IUserDto, int> _userManager;
        private readonly IEmailBuilder _emailBuilder;

        public EmailService(UserManager<IUserDto, int> userManager, IEmailBuilder emailBuilder)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (emailBuilder == null)
            {
                throw new ArgumentNullException("emailBuilder");
            }

            _userManager = userManager;
            _emailBuilder = emailBuilder;
        }

        public async Task SendUserRegistrationConfirmationEmailAsync(IUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user.UserId);

            Email email = await _emailBuilder.BuildRegsitrationConfirmationEmailAsync(user, token);

            await _userManager.SendEmailAsync(user.UserId, email.Subject, email.Body);
        }

        public async Task SendResetUserPasswordEmailAsync(IUserDto user)
        {
            string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user.UserId);

            Email email = await _emailBuilder.BuildResetPasswordEmailAsync(user, resetPasswordToken);

            await _userManager.SendEmailAsync(user.UserId, email.Subject, email.Body);
        }
    }
}