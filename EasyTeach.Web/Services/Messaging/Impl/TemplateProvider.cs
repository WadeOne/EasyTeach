using System;
using System.IO;
using System.Web;

namespace EasyTeach.Web.Services.Messaging.Impl
{
    public class TemplateProvider
    {
        public virtual StreamReader GetTemplate(TemplateType templateType)
        {
            switch (templateType)
            {
                case TemplateType.EmailConfirmation:
                    return OpenStreamReader("~/App_Data/EmailTemplates/RegistrationConfirmationEmail.txt");

                case TemplateType.ResetPassword:
                    return OpenStreamReader("~/App_Data/EmailTemplates/ResetPasswordEmail.txt");

                default:
                    throw new NotSupportedException(String.Format("Template type '{0}' is not supported", templateType));
            }
        }

        private static StreamReader OpenStreamReader(string path)
        {
            return File.OpenText(HttpContext.Current.Server.MapPath(path));
        }
    }
}