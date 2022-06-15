using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;


namespace Api
{
    public static class Program
    {
        public static async Task Main(string[] args) {
            var app = CreateAppHost(args);
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                ConfigureWebAppForDevelopment(app);
            }

            app.UseHttpsRedirection()
               .UseAuthorization();
                
            app.MapControllers();

            Console.WriteLine("Starting the API");
            
            await app.RunAsync();

            Console.WriteLine("Shutting down the API");
        }

        private static WebApplication CreateAppHost(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                .AddSwaggerGen()
                .AddEndpointsApiExplorer()
                .AddAutofac()
                .AddControllers();

            return builder.Build();
        }

        private static void ConfigureWebAppForDevelopment(IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
