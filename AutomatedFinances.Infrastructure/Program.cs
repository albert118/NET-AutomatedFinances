﻿namespace AutomatedFinances.Infrastructure
{
    internal class Program
    {
        private static async Task Main(string[] args) {
            Console.WriteLine("AutoFac Setup running...");

            var host = CreateConsoleHost(args);

            Console.WriteLine("Starting the console app...");
            await host.RunConsoleAsync();

            // end of lifetime
            Console.WriteLine("Shutting down the console app...");
            // container.Dispose();
        }

        private static IHostBuilder CreateConsoleHost(string[] args) {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
#if DEBUG
                .UseEnvironment("development");
#else
                .UseEnvironment("production");
#endif
            return hostBuilder;
        }
    }
}