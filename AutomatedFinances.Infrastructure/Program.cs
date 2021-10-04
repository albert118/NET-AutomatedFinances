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


        private static async Task Main(string[] args)
        {
            var serverSettings = new ServerSettings();

            // start a new container with the server settings configuration.
            Console.WriteLine("AutoFac Setup running...");
            var container = SetupAutofacContainer(serverSettings);
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
            // TODO: Move these to ServerSettings!
            var dbSettings = new DatabaseSettings
            {
                Server = "192.168.133",
                Password = "p9kNCTHi@91a",
                Database = "AutomatedFinancesDb",
                UserName = "sa"
            };

            containerBuilder.AddEntityFramework();

            containerBuilder
                .RegisterInstance(dbSettings)
                .AsSelf()
                .SingleInstance();
            containerBuilder
                .RegisterType<AutomedFinancesDbContext>()
                .As<IAutomedFinancesDbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
