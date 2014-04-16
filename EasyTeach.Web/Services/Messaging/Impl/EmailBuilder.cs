using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;

namespace EasyTeach.Web.Services.Messaging.Impl
{
    public sealed class EmailBuilder : IEmailBuilder
    {
        private readonly TemplateProvider _templateProvider;

        public EmailBuilder(TemplateProvider templateProvider)
        {
            if (templateProvider == null)
            {
                throw new ArgumentNullException("templateProvider");
            }

            _templateProvider = templateProvider;
        }

        public async Task<Email> BuildRegsitrationConfirmationEmailAsync(IUserDto user, string confirmEmailToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(confirmEmailToken))
            {
                throw new ArgumentNullException("confirmEmailToken");
            }

            Email email = await BuildEmail(user, TemplateType.EmailConfirmation, confirmEmailToken);
            return email;
        }

        private async Task<Email> BuildEmail(IUserDto user, TemplateType templateType, string token)
        {
            var email = new Email { Subject = String.Empty };
            var bodyBuilder = new StringBuilder(200);

            using (StreamReader reader = _templateProvider.GetTemplate(templateType))
            {
                string line = await reader.ReadLineAsync();
                for (int i = 0; line != null; i++)
                {
                    if (i == 0)
                    {
                        email.Subject = ReplaceMetaTagsWithUserData(line, user, token);
                    }
                    else
                    {
                        bodyBuilder.AppendLine(ReplaceMetaTagsWithUserData(line, user, token));
                    }

                    line = await reader.ReadLineAsync();
                }
            }

            email.Body = bodyBuilder.ToString();
            return email;
        }

        public async Task<Email> BuildResetPasswordEmailAsync(IUserDto user, string resetPasswordToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(resetPasswordToken))
            {
                throw new ArgumentNullException("resetPasswordToken");
            }

            Email email = await BuildEmail(user, TemplateType.ResetPassword, resetPasswordToken);
            return email;
        }

        private string ReplaceMetaTagsWithUserData(string line, IUserDto userDto, string token)
        {
            return line.Replace("{{FirstName}}", userDto.FirstName)
                       .Replace("{{LastName}}", userDto.LastName)
                       .Replace("{{GroupNumber}}", userDto.Group.GroupNumber.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{GroupYear}}", userDto.Group.Year.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{Email}}", userDto.Email)
                       .Replace("{{Token}}", token)
                       .Replace("{{UserId}}", userDto.UserId.ToString(CultureInfo.InvariantCulture));
        }
    }
}