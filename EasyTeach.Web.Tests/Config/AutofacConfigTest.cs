using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Core;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Data.Context;
using EasyTeach.Web.Controllers;
using Microsoft.AspNet.Identity;
using Xunit;
using Xunit.Extensions;

namespace EasyTeach.Web.Tests.Config
{
    public sealed class AutofacConfigTest
    {
        private readonly IDependencyResolver _resolver;

        public AutofacConfigTest()
        {
            AutofacConfig.RegisterDependencies(builder => builder.RegisterType<EasyTeachContext>().AsSelf());
            _resolver = GlobalConfiguration.Configuration.DependencyResolver;
        }

        [Theory]
        [InlineData(typeof(UserManager<IUserDto, int>))]
        [InlineData(typeof(UserStore))]
        [InlineData(typeof(IUserDtoMapper))]
        [InlineData(typeof(UserTokenProvider))]
        [InlineData(typeof(EasyTeachContext))]
        public void RegisterDependencies_ResolvedTypeCorrectly(Type typeToResolve)
        {
            object instance = _resolver.GetService(typeToResolve);
            Assert.NotNull(instance);
            Assert.IsAssignableFrom(typeToResolve, instance);
        }
    }
}
