using Autofac;
using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;
using Microsoft.EntityFrameworkCore;

namespace AppHost;

internal static class EfRegistrationExtensions
{
    public static ContainerBuilder AddTradingTransactionContext(this ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddDbContextOptions<TradingTransactionReadDbContext>()
            .AddDbContextOptions<TradingTransactionWriteDbContext>();

        containerBuilder
            .RegisterType<TradingTransactionReadDbContext>()
            .As<ITradingTransactionReadDbContext>()
            .InstancePerLifetimeScope();

        containerBuilder
            .RegisterType<TradingTransactionWriteDbContext>()
            .As<ITradingTransactionWriteDbContext>()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    private static ContainerBuilder AddDbContextOptions<TContext>(this ContainerBuilder containerBuilder)
        where TContext : DbContext
    {
        // TODO: add this is as a DI model and inject it in the service provider (SP) delegate
        containerBuilder.Register(sp =>
            {
                var loggerFactory = sp.Resolve<ILoggerFactory>();
                var dbSettings = sp.Resolve<DatabaseSettings>();
                return new DbContextOptionsBuilder<TContext>()
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer(dbSettings.ConnectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .Options; // make sure to return options here! Otherwise we'll register the builder
            })
            .AsSelf()
            .SingleInstance();

        return containerBuilder;
    }
}