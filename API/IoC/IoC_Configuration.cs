using Autofac;
using Business.Services;
using Data;
using Data.Repositories;
using Domain.DbContext;
using Domain.RepositoryContracts;
using Domain.ServiceContracts;

namespace API.IoC
{
    public class IoC_Configuration : IIoC_Configuration
    {
        public IContainer Container(ContainerBuilder builder)
        {
            builder.RegisterType<AppContext>().As<IContext>();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>();

            return builder.Build();
        }
    }
}