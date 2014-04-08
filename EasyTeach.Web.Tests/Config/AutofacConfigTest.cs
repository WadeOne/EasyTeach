using System;
using System.Web.Http;

using Autofac.Core;

using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Data.Context;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Repostitories.Mappers;

using Xunit;

namespace EasyTeach.Web.Tests.Config
{
    public class AutofacConfigTest
    {
        public AutofacConfigTest()
        {
            AutofacConfig.RegisterDependencies();
        }

        [Fact]
        public void ClassesRegistered_ResolvedCorrectly()
        {
            var resolver = GlobalConfiguration.Configuration.DependencyResolver;

            var instance = resolver.GetService(typeof(IUserDtoMapper));

            Assert.NotNull(instance);
            Assert.True(instance is UserDtoMapper);
        }

        [Fact]
        public void ClassesRegistered_RequestedInstanceRegisteredAsPerApiRequest_ExceptionThrown()
        {
            var resolver = GlobalConfiguration.Configuration.DependencyResolver;

            Assert.Throws<DependencyResolutionException>(() => resolver.GetService(typeof(EasyTeachContext)));
        }
    }
}
