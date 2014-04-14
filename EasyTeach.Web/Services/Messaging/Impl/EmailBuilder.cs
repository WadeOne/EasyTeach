using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;

namespace EasyTeach.Web.Services.Messaging.Impl
{
    public sealed class EmailBuilder : IEmailBuilder
    {
        private readonly UrlHelper _urlHelper;
        private readonly TemplateProvider _templateProvider;

        public EmailBuilder(UrlHelper urlHelper, TemplateProvider templateProvider)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            if (templateProvider == null)
            {
                throw new ArgumentNullException("templateProvider");
            }

            _urlHelper = urlHelper;
            _templateProvider = templateProvider;
        }

        public async Task<Email> BuildRegsitrationConfirmationEmailAsync(IUserDto user, string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("token");
            }

            Email email = await BuildEmail(user, TemplateType.EmailConfirmation);

            // TODO replace URL meta tag
            return email;
        }

        private async Task<Email> BuildEmail(IUserDto user, TemplateType templateType)
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
                        email.Subject = ReplaceMetaTagsWithUserData(line, user);
                    }
                    else
                    {
                        bodyBuilder.AppendLine(ReplaceMetaTagsWithUserData(line, user));
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

            Email email = await BuildEmail(user, TemplateType.ResetPassword);

            // TODO replace URL meta tag
            return email;
        }

        private string ReplaceMetaTagsWithUserData(string line, IUserDto userDto)
        {
            return line.Replace("{{FirstName}}", userDto.FirstName)
                       .Replace("{{LastName}}", userDto.LastName)
                       .Replace("{{GroupNumber}}", userDto.Group.GroupNumber.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{GroupYear}}", userDto.Group.Year.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{Email}}", userDto.Email);
        }
    }
}