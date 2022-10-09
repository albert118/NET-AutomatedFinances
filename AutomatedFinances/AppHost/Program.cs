using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutomatedFinances.Application;

namespace AppHost;

internal static class Program
{
    private static void Main()
    {
        var appConfiguration = GetAppConfiguration();

        var webApplicationBuilder = WebApplication.CreateBuilder();

        webApplicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        webApplicationBuilder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder
                .AddDatabaseSettings(appConfiguration)
                .AddEfDbContexts()
                .AddApplicationServices();
        });

        ConfigureControllers(webApplicationBuilder);

        var webApplication = webApplicationBuilder.Build();

        ConfigureWebApp(webApplication);

        webApplication.Run();
    }

    private static void ConfigureControllers(WebApplicationBuilder webApplicationBuilder)
    {
        // Add services to the container.
        webApplicationBuilder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        webApplicationBuilder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }

    private static void ConfigureWebApp(WebApplication webApplication) {
        // Configure the HTTP request pipeline.
        if (webApplication.Environment.IsDevelopment()) {
            webApplication
                .UseSwagger()
                .UseSwaggerUI();
        }

        webApplication
            .UseHttpsRedirection()
            .UseAuthorization();

        webApplication.MapControllers();
    }

    private static ContainerBuilder AddEfDbContexts(this ContainerBuilder containerBuilder)
    {
        return containerBuilder
            .AddTransactionContext();
    }

    private static ContainerBuilder AddDatabaseSettings(this ContainerBuilder containerBuilder,
        IConfiguration appConfiguration) {

        var databaseSettings = new DatabaseSettings(
            appConfiguration.GetConnectionString("IridiumDbConnection"),
            new (new Version(
                int.Parse(appConfiguration.GetSection("ConnectionStrings:ServerVersionMajor").Value),
                int.Parse(appConfiguration.GetSection("ConnectionStrings:ServerVersionMinor").Value),
                int.Parse(appConfiguration.GetSection("ConnectionStrings:ServerVersionBuild").Value)
            ))
        );

        containerBuilder.RegisterInstance(databaseSettings).AsSelf().SingleInstance();

        return containerBuilder;
    }

    private static IConfigurationRoot GetAppConfiguration()
    {
        const string appSettingsFilePath = "appsettings.json";

        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appSettingsFilePath)
            .Build();
    }
}
