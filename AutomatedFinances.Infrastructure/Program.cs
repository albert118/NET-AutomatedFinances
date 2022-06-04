using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure
{
    internal static class Program
    {
        private static async Task Main(string[] args) {
            Console.WriteLine("Setting up");

            var host = CreateConsoleHost(args);

            Console.WriteLine("Starting the console app");
            
            await host.RunConsoleAsync();

            Console.WriteLine("Shutting down the console app");
        }

        private static IHostBuilder CreateConsoleHost(string[] args) {
            var builder = Host.CreateDefaultBuilder(args)
            
            #if DEBUG
                    .UseEnvironment("development")
            #else
                .UseEnvironment("production")
            #endif
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services => {
                    services.AddDbContext<IridiumDbContext>();
                });

            return builder;
        }
    }
}