using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using EasyTeach.Web.Controllers;
using Xunit;

namespace EasyTeach.Web.Tests.Controllers
{
    public sealed class ControllerTest
    {
        [Fact]
        public void AllWebApiControllersMustBeDericedFromApiControllerBase()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UserController));
            var controllers =
                assembly.GetExportedTypes()
                    .Where(
                        t =>
                            t.IsClass &&
                            !t.IsAbstract &&
                            t.Name.EndsWith("Controller") &&
                            t.IsAssignableTo<ApiController>());

            foreach (Type controller in controllers)
            {
                Assert.True(controller.IsAssignableTo<ApiControllerBase>(),
                    String.Format("Api controller '{0}' doesn't derived from '{1}'",
                    controller,
                    typeof(ApiControllerBase)));
            }
        }
    }
}