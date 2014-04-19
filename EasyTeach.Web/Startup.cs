using System;
using System.Web.Http;
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
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(GlobalConfiguration.Configuration.DependencyResolver),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOAuthBearerTokens(oAuthOptions);
        }
    }
}
