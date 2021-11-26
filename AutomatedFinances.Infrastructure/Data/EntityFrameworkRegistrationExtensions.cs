using Autofac;
using AutomatedFinances.Infrastructure.Data.ExpendituresDb;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Infrastructure.Data
{
    internal static class EntityFrameworkRegistrationExtensions
    {
        internal static ContainerBuilder AddEntityFramework(
            this ContainerBuilder containerBuilder) => containerBuilder
                .AddExpendituresDb();

        private static ContainerBuilder AddExpendituresDb(this ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDbContextOptions<ExpenditureReadDbContext>();
            containerBuilder.AddDbContextOptions<ExpenditureWriteDbContext>();

            containerBuilder
                .RegisterType<ExpenditureReadDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder
                .RegisterType<ExpenditureWriteDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            return containerBuilder;
        }

        private static ContainerBuilder AddDbContextOptions<TContext>(
            this ContainerBuilder containerBuilder)
            where TContext : DbContext
        {
            containerBuilder
                .Register(sp =>
                {
                    var settings = sp.Resolve<DatabaseSettings>();
                    return BuildDbContextOptions<TContext>(
                        settings.ConnectionString,
                        settings.CmdTimeout);
                })
                .AsSelf()
                .SingleInstance();
            return containerBuilder;
        }

        private static DbContextOptionsBuilder<TDbContext> BuildDbContextOptions<TDbContext>(
            string connString,
            int commandTimeout) where TDbContext : DbContext => new DbContextOptionsBuilder<TDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseSqlServer(connString, sqlServerOptionsAction => sqlServerOptionsAction
                    .CommandTimeout(commandTimeout));
    }
}
