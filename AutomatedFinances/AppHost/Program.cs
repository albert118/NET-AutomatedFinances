using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;
using Microsoft.EntityFrameworkCore;

namespace AppHost;

internal static class Program
{
    private const string AppSettingsFilePath = "appsettings.json";

    private static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder
                .RegisterType<TradingTransactionReadDbContext>()
                .WithParameter("options", GetDbContextOptions<TradingTransactionReadDbContext>())
                .UsingConstructor(typeof(DbContextOptions<TradingTransactionReadDbContext>))
                .As<ITradingTransactionReadDbContext>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<TradingTransactionWriteDbContext>()
                .WithParameter("options", GetDbContextOptions<TradingTransactionWriteDbContext>())
                .UsingConstructor(typeof(DbContextOptions<TradingTransactionWriteDbContext>))
                .As<ITradingTransactionWriteDbContext>()
                .InstancePerLifetimeScope();
        });

        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    // private static ContainerBuilder AddDbContextOptions<TContext>(this ContainerBuilder containerBuilder)
    //     where TContext : DbContext
    // {
    //     // TODO: add this is as a DI model and inject it in the service provider (SP) delegate
    //     var configuration = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile(AppSettingsFilePath)
    //         .Build();
    //
    //     containerBuilder.Register(sp =>
    //     {
    //         var loggerFactory = sp.Resolve<ILoggerFactory>();
    //
    //         return new DbContextOptionsBuilder<TContext>()
    //             .UseLoggerFactory(loggerFactory)
    //             .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"))
    //             .EnableDetailedErrors()
    //             .EnableSensitiveDataLogging();
    //     }).AsSelf().SingleInstance();
    //
    //     return containerBuilder;
    // }

    private static DbContextOptions GetDbContextOptions<TContext>() where TContext : DbContext
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFilePath)
            .Build();

        var builder = new DbContextOptionsBuilder<TContext>()
            .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();

        return builder.Options;
    }
}
