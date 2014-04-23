using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
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
                .Except<EasyTeachContext>(x => x.AsSelf()) //TODO: find a way to create EF context one per API request
                .Except<XmlDocumentationProvider>();

            builder.Register<Func<IAuthenticationManager>>(c => () => c.Resolve<HttpRequestMessage>().GetOwinContext().Authentication).InstancePerApiRequest();
            
            builder.Register<Func<object, ValidationContext>>(c => o => new ValidationContext(o, new Adapter(), null));
            
            builder.RegisterType<UserManager<IUserDto, int>>().AsSelf().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);

            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);

            if (beforeBuild != null)
            {
                beforeBuild(builder);
            }

            IContainer container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private sealed class Adapter : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                var message = (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];

                return message.GetDependencyScope().GetService(serviceType);
            }
        }
    }
}