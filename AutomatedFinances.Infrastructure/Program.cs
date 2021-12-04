using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Infrastructure.Data;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure
{
    internal class Program
    {
        const string ServiceName = "automatedfinances";
        
        private static readonly ServerSettings ServerSettings = ConfigureServerSettings();

        private static async Task Main(string[] args) {
            

            // start a new container with the server settings configuration.
            Console.WriteLine("AutoFac Setup running...");
            var container = SetupAutofacContainer(ServerSettings);
            container.BeginLifetimeScope();
            
            var host = CreateConsoleHost(args);
            
            Console.WriteLine("Starting the console app...");
            await host.RunConsoleAsync();

            // end of lifetime
            Console.WriteLine("Shutting down the console app...");
            container.Dispose();
        }

        private static IHostBuilder CreateConsoleHost(string[] args) 
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
#if DEBUG
                .UseEnvironment("development");
#else
                .UseEnvironment("production");
#endif
            return hostBuilder;
        }

        private static IContainer SetupAutofacContainer(ServerSettings serverSettings)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterInstance(serverSettings)
                .AsSelf()
                .SingleInstance();

            RegisterAndConfigureDatabase(builder, serverSettings);

            return builder.Build();
        }

        private static void RegisterAndConfigureDatabase(
            ContainerBuilder containerBuilder, 
            ServerSettings serverSettings)
        {
            containerBuilder.AddEntityFramework();

            containerBuilder
                .RegisterInstance(serverSettings.ToDataBaseSettings())
                .AsSelf()
                .SingleInstance();
            
            containerBuilder
                .RegisterType<AutomedFinancesDbContext>()
                .As<IAutomedFinancesDbContext>()
                .InstancePerLifetimeScope();
        }

        private static ServerSettings ConfigureServerSettings() =>
            new() {
                IridiumServerPath = "192.168.133",
                AutomatedFinancesDbName = "AutomatedFinancesDb",
                AutomatedFinancesDbPassword = "p9kNCTHi@91a",
                AutomatedFinancesDbUser = "sa"
            };
    }
}
