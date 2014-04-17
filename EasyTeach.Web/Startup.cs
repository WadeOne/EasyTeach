using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Web.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

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
                Provider = new ApplicationOAuthProvider(CreateUserService),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOAuthBearerTokens(oAuthOptions);
        }

        private IUserService CreateUserService()
        {
            IDependencyResolver resolver = GlobalConfiguration.Configuration.DependencyResolver;
            if (resolver == null)
            {
                throw new InvalidOperationException("Dependency resolver is not configured");
            }

            using (ILifetimeScope scope = resolver.GetRootLifetimeScope())
            {
                return scope.Resolve<IUserService>();
            }
        }
    }
}
