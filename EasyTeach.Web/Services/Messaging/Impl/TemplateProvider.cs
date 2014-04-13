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
                    return
                        File.OpenText(
                            HttpContext.Current.Server.MapPath(
                                "~/App_Data/EmailTemplates/RegistrationConfirmationEmail.txt"));

                default:
                    throw new NotSupportedException(String.Format("Template type '{0}' is not supported", templateType));
            }
        }
    }
}