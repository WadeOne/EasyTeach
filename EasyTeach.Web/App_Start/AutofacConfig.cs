using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;

using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Data.Context;
using EasyTeach.Web.Controllers;

using Microsoft.Owin.Security;

namespace EasyTeach.Web
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            //var request = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(
                Assembly.Load("EasyTeach.Web"),
                Assembly.Load("EasyTeach.Data"),
                Assembly.Load("EasyTeach.Core"))
                .AsImplementedInterfaces()
                .AsSelf()
                .Except<EasyTeachContext>(x => x.AsSelf().InstancePerApiRequest())
                //.Except<UserController>(x => x.AsSelf().InstancePerApiRequest()).UsingConstructor(typeof(IUserService), typeof(IAuthenticationManager)).WithParameter(new NamedParameter("authenticationManagerFactory", request.GetOwinContext().Authentication), new ResolvedParameter())
                ;
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}