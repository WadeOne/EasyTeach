using System;
using System.Web.Http;
using System.Web.Http.Dependencies;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Web.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(EasyTeach.Web.Startup))]

namespace EasyTeach.Web
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthetication(app);
        }

        private void ConfigureAuthetication(IAppBuilder app)
        {
            IDependencyResolver resolver = GlobalConfiguration.Configuration.DependencyResolver;
            if (resolver == null)
            {
                throw new InvalidOperationException("Dependency resolver is not configured");
            }

            var userService = (IUserService)resolver.GetService(typeof (IUserService));

            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(userService),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOAuthBearerTokens(oAuthOptions);
        }
    }
}
