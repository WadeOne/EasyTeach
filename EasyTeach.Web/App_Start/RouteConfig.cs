using System.Web.Mvc;
using System.Web.Routing;

namespace EasyTeach.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{*catchall}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );
        }
    }
}
