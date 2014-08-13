using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Core.Services.Dashboard.Impl;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Core.Validation.EntityValidator;
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
                .Except<XmlDocumentationProvider>()
                .Except<LessonService>()
                .Except<GroupService>()
                .Except<VisitService>()
                .Except<ScoreService>()
                .Except<AuthLessonServiceWrapper>()
                .Except<LogLessonServiceWrapper>()
                .Except<AuthGroupServiceWrapper>()
                .Except<LogGroupServiceWrapper>()
                .Except<AuthVisitServiceWrapper>()
                .Except<AuthScoreServiceWrapper>();

            builder.Register<Func<IAuthenticationManager>>(c => () =>
            {
                HttpRequestMessage message = ResolveRequestMessage();
                return message.GetOwinContext().Authentication;
            }).InstancePerRequest();

            builder.Register(c =>
            {
                HttpRequestMessage message = ResolveRequestMessage();
                return (ClaimsPrincipal)message.GetOwinContext().Request.User;
            });
            builder.Register<Func<object, ValidationContext>>(c => o => new ValidationContext(o, new Adapter(), null));
            builder.RegisterType<UserManager<IUserDto, int>>().AsSelf().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);

            builder.RegisterType<LessonService>();
            builder.Register<AuthLessonServiceWrapper>(c =>
                new AuthLessonServiceWrapper(
                    c.Resolve<LessonService>(),
                        c.Resolve<ClaimsPrincipal>(),
                        c.Resolve<EntityValidator>(),
                        c.Resolve<IUserStore<IUserDto, int>>(),
                        c.Resolve<Core.Security.ClaimsAuthorizationManager>()
                    ));
            builder.Register<ILessonService>(c => new LogLessonServiceWrapper(c.Resolve<AuthLessonServiceWrapper>()));

            builder.RegisterType<GroupService>();
            builder.Register<AuthGroupServiceWrapper>(c =>
                new AuthGroupServiceWrapper(
                    c.Resolve<GroupService>(),
                        c.Resolve<ClaimsPrincipal>(),
                        c.Resolve<EntityValidator>(),
                        c.Resolve<IUserStore<IUserDto, int>>(),
                        c.Resolve<Core.Security.ClaimsAuthorizationManager>()
                    ));
            builder.Register<IGroupService>(c => new LogGroupServiceWrapper(c.Resolve<AuthGroupServiceWrapper>()));

            //builder.RegisterType<LessonService>().Named<ILessonService>("lessonService");
            //builder.RegisterDecorator<ILessonService>(
            //    (c, inner) =>
            //        new AuthLessonServiceWrapper(
            //            inner,
            //            c.Resolve<ClaimsPrincipal>(),
            //            c.Resolve<EntityValidator>(),
            //            c.Resolve<IUserStore<IUserDto, int>>(),
            //            c.Resolve<Core.Security.ClaimsAuthorizationManager>()),
            //    "lessonService");

            //builder.RegisterDecorator<IGroupService>(
            //    (c, inner) =>
            //    {
            //        return new AuthGroupServiceWrapper(
            //            inner,
            //            c.Resolve<ClaimsPrincipal>(),
            //            c.Resolve<EntityValidator>(),
            //            c.Resolve<IUserStore<IUserDto, int>>(),
            //            c.Resolve<Core.Security.ClaimsAuthorizationManager>());
            //    }, "authGroupsServiceWrapper", "groupService").Named<IGroupService>("logGroupsServiceWrapper");
            //builder.RegisterDecorator<IGroupService>(
            //    (c, inner) => {
            //                      return new LogGroupServiceWrapper(inner);
            //    }, null, "logGroupsServiceWrapper");

            
            //builder.RegisterType<GroupService>().Named<IGroupService>("groupService");
            //builder.RegisterDecorator<IGroupService>(
            //    (c, inner) =>
            //        new AuthGroupServiceWrapper(
            //            inner,
            //            c.Resolve<ClaimsPrincipal>(),
            //            c.Resolve<EntityValidator>(),
            //            c.Resolve<IUserStore<IUserDto, int>>(),
            //            c.Resolve<Core.Security.ClaimsAuthorizationManager>()),
            //    "groupService");

            builder.RegisterType<VisitService>().Named<IVisitService>("visitService");
            builder.RegisterDecorator<IVisitService>(
                (c, inner) =>
                    new AuthVisitServiceWrapper(
                        inner,
                        c.Resolve<ClaimsPrincipal>(),
                        c.Resolve<EntityValidator>(),
                        c.Resolve<IUserStore<IUserDto, int>>(),
                        c.Resolve<Core.Security.ClaimsAuthorizationManager>()),
                "visitService");
            
            builder.RegisterType<ScoreService>().Named<IScoreService>("scoreService");
            builder.RegisterDecorator<IScoreService>(
                (c, inner) =>
                    new AuthScoreServiceWrapper(
                        inner,
                        c.Resolve<ClaimsPrincipal>(),
                        c.Resolve<EntityValidator>(),
                        c.Resolve<IUserStore<IUserDto, int>>(),
                        c.Resolve<Core.Security.ClaimsAuthorizationManager>()),
                "scoreService");
            
            if (beforeBuild != null)
            {
                beforeBuild(builder);
            }

            IContainer container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static HttpRequestMessage ResolveRequestMessage()
        {
            return (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
        }

        private sealed class Adapter : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                HttpRequestMessage message = ResolveRequestMessage();
                return message.GetDependencyScope().GetService(serviceType);
            }
        }
    }
}