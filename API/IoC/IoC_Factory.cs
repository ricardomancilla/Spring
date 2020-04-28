using API.Mapper;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace API.IoC
{
    public class IoC_Factory
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new MapperInstaller());

            IContainer container = new IoC_Configuration().Container(builder);
            return container;
        }
    }
}