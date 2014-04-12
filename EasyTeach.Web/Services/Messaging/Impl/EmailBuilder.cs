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

            var email = new Email { Subject = String.Empty };
            var bodyBuilder = new StringBuilder(200);

            using (StreamReader reader = _templateProvider.GetTemplate(TemplateType.EmailConfirmation))
            {
                string line = await reader.ReadLineAsync();
                for (int i = 0; line != null; i++)
                {
                    if (i == 0)
                    {
                        email.Subject = ReplaceMetaTagsWithData(line, user, token);
                    }
                    else
                    {
                        bodyBuilder.AppendLine(ReplaceMetaTagsWithData(line, user, token));
                    }

                    line = await reader.ReadLineAsync();
                }
            }

            email.Body = bodyBuilder.ToString();
            return email;
        }

        private string ReplaceMetaTagsWithData(string line, IUserDto userDto, string token)
        {
            return line.Replace("{{FirstName}}", userDto.FirstName)
                       .Replace("{{LastName}}", userDto.LastName)
                       .Replace("{{GroupNumber}}", userDto.Group.GroupNumber.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{GroupYear}}", userDto.Group.Year.ToString(CultureInfo.InvariantCulture))
                       .Replace("{{Email}}", userDto.Email)
                       .Replace("{{ConfirmationUrl}}", _urlHelper.Link("DefaultApi", new { controller = "User", action = "ConfirmEmail", confirmEmailToken = token, userId = userDto.UserId }));
        }

        public Task<Email> BuildResetPasswordConfirmationEmailAsync(IUserDto user, string token)
        {
            throw new NotImplementedException();
        }
    }
}