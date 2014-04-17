using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging.Impl;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Data.Context;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Repostitories.Mappers;
using EasyTeach.Web.Providers;
using EasyTeach.Web.Services.Messaging.Impl;
using Microsoft.AspNet.Identity;
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
            // TODO: resolve dependency
            var manager = new UserManager<IUserDto, int>(new UserStore(new UserRepository(new EasyTeachContext())));

            return
                new UserService(manager, new UserDtoMapper(),
                    new EmailService(manager, new EmailBuilder(new TemplateProvider())));
            /*
            IDependencyResolver resolver = GlobalConfiguration.Configuration.DependencyResolver;
            if (resolver == null)
            {
                throw new InvalidOperationException("Dependency resolver is not configured");
            }

            using (ILifetimeScope scope = resolver.GetRootLifetimeScope())
            {
                return scope.Resolve<IUserService>();
            }*/
        }
    }
}
