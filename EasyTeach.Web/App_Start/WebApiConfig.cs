using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using EasyTeach.Web.Filters;
using EasyTeach.Web.Models.ViewModels.Dashboard.Lessons;
using EasyTeach.Web.Models.ViewModels.Dashboard.Scores;
using EasyTeach.Web.Models.ViewModels.Groups;
using Newtonsoft.Json.Serialization;

namespace EasyTeach.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new SecurityExceptionFilterAttribute());

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<GroupViewModel>("Groups");
            builder.EntitySet<LessonViewModel>("Lessons");
            builder.EntitySet<ScoreViewModel>("Scores");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
