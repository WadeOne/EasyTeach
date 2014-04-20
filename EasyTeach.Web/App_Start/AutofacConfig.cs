using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Validation.Attributes;
using EasyTeach.Data.Context;
using EasyTeach.Web.Areas.HelpPage;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using UrlHelper = System.Web.Http.Routing.UrlHelper;

namespace EasyTeach.Web
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(Action<ContainerBuilder> beforeBuild = null)
        {
            var builder = new ContainerBuilder();

            
            IServiceProvider serviceProvider = new ServiceContainer();

            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);

            builder.RegisterAssemblyTypes(
                Assembly.Load("EasyTeach.Web"),
                Assembly.Load("EasyTeach.Data"),
                Assembly.Load("EasyTeach.Core"))
                .AsImplementedInterfaces()
                .AsSelf()
                .Except<EasyTeachContext>(x => x.AsSelf())
                .Except<XmlDocumentationProvider>();

            builder.RegisterType<UniqueEmailAttribute>().AsSelf().PropertiesAutowired();
            builder.Register<Func<IAuthenticationManager>>(c => () => c.Resolve<HttpRequestMessage>().GetOwinContext().Authentication).InstancePerApiRequest();
            builder.Register<Func<object, ValidationContext>>(c => o => new ValidationContext(o, serviceProvider, null));
            builder.RegisterType<UserManager<IUserDto, int>>().AsSelf().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            builder.Register(c => new UrlHelper(c.Resolve<HttpRequestMessage>())).InstancePerApiRequest();

            if (beforeBuild != null)
            {
                beforeBuild(builder);
            }

            IContainer container = builder.Build();
            serviceProvider = (IServiceProvider)container;
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //serviceProvider.AddService(typeof(IUserRepository), (serviceContainer, type) => container.Resolve<IUserRepository>());
        }
    }
}