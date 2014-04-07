using Autofac;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Repostitories.Mappers;

namespace EasyTeach.Web
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            // TODO: add unit test for resolving dependecies
            var builder = new ContainerBuilder();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserDtoMapper>().As<IUserDtoMapper>();
        }
    }
}