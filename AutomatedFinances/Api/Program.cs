using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace Api;

public static class Program
{
    public static async Task Main() {
        var containerBuilder = new ContainerBuilder();
        var hostBuilder = WebApplication.CreateBuilder();

        hostBuilder
            .SetUpAppHost();

        containerBuilder.SetUpContainer();

        var webHost = hostBuilder.Build();

        webHost.MapControllers();
        webHost.UseHttpsRedirection().UseAuthorization();

        await webHost.RunAsync();
    }

    private static WebApplicationBuilder SetUpAppHost(this WebApplicationBuilder hostBuilder) {
        hostBuilder.Services
            .AddEndpointsApiExplorer()
            .AddAutofac()
            .AddControllers();

        return hostBuilder;
    }

    private static WebApplicationBuilder AddAutofacToWebApp(this WebApplicationBuilder hostBuilder)
    {
        hostBuilder.

            .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        return hostBuilder;
    }

    private static void SetUpContainer(this ContainerBuilder containerBuilder)
    {
        containerBuilder.Regi
    }
}
