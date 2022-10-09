using Autofac;
using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Infrastructure.Data.TransactionRecordsDb;
using Microsoft.EntityFrameworkCore;

namespace AppHost;

internal static class EfRegistrationExtensions
{
    public static ContainerBuilder AddTransactionContext(this ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddDbContextOptions<TransactionReadDbContext>()
            .AddDbContextOptions<TransactionWriteDbContext>();

        containerBuilder
            .RegisterType<TransactionReadDbContext>()
            .As<ITransactionReadDbContext>()
            .InstancePerLifetimeScope();

        containerBuilder
            .RegisterType<TransactionWriteDbContext>()
            .As<ITransactionWriteDbContext>()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    private static ContainerBuilder AddDbContextOptions<TContext>(this ContainerBuilder containerBuilder)
        where TContext : DbContext
    {
        containerBuilder.Register(sp =>
            {
                var loggerFactory = sp.Resolve<ILoggerFactory>();
                var dbSettings = sp.Resolve<DatabaseSettings>();
                return new DbContextOptionsBuilder<TContext>()
                    .UseLoggerFactory(loggerFactory)
                    .UseMySql(dbSettings.ConnectionString, dbSettings.ServerVersion)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .Options; // make sure to return options here! Otherwise we'll register the builder
            })
            .AsSelf()
            .SingleInstance();

        return containerBuilder;
    }
}
