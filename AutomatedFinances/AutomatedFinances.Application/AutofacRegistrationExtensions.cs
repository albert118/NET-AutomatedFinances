using Autofac;
using System.Reflection;

namespace AutomatedFinances.Application;

public static class AutofacRegistrationExtensions
{
    public static ContainerBuilder AddApplicationServices(this ContainerBuilder containerBuilder)
    {
        return containerBuilder.RegisterSimpleAttributedServices(typeof(AutofacRegistrationExtensions).Assembly);
    }

    private static ContainerBuilder RegisterSimpleAttributedServices(this ContainerBuilder containerBuilder,
        Assembly assembly)
    {
        containerBuilder.RegisterAssemblyTypes(assembly)
            .Where(type => type.GetCustomAttributes(typeof(InstanceScopedServiceAttribute), inherit: false).Any())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }
}
