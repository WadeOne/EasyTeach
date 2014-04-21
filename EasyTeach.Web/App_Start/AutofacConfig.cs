using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Data.Context;
using EasyTeach.Web.Areas.HelpPage;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EasyTeach.Web
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(Action<ContainerBuilder> beforeBuild = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);

            builder.RegisterAssemblyTypes(
                Assembly.Load("EasyTeach.Web"),
                Assembly.Load("EasyTeach.Data"),
                Assembly.Load("EasyTeach.Core"))
                .AsImplementedInterfaces()
                .AsSelf()
                .Except<EasyTeachContext>(x => x.AsSelf().InstancePerApiRequest())
                .Except<XmlDocumentationProvider>();

            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);
            builder.Register<Func<IAuthenticationManager>>(c => () => c.Resolve<HttpRequestMessage>().GetOwinContext().Authentication).InstancePerApiRequest();
            builder.RegisterType<UserManager<IUserDto, int>>().AsSelf().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);

            if (beforeBuild != null)
            {
                beforeBuild(builder);
            }

            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}