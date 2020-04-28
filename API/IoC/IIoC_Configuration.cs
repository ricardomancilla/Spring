using Autofac;

namespace API.IoC
{
    public interface IIoC_Configuration
    {
        IContainer Container(ContainerBuilder builder);
    }
}
