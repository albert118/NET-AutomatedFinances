using Autofac;
using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure;

internal static class Program
{
    private const string AppSettingsFilePath = "appsettings.json";

    private static async Task Main()
    {
        var container = BuildContainer();

        using var scope = container.BeginLifetimeScope();
        var t = scope.Resolve<ThisControllerThing>();
        t.DoSomeDatabaseStuff();
    }

    private static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<TradingTransactionWriteDbContext>()
            .WithParameter("options", GetWriteDbContextOptions())
            .UsingConstructor(typeof(DbContextOptions<TradingTransactionWriteDbContext>))
            .As<ITradingTransactionWriteDbContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ThisControllerThing>().AsSelf();

        return builder.Build();
    }

    private static DbContextOptions<TradingTransactionWriteDbContext> GetWriteDbContextOptions()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFilePath)
            .Build();

        var builder = new DbContextOptionsBuilder<TradingTransactionWriteDbContext>()
            .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"));

        return builder.Options;
    }
}

public class ThisControllerThing
{
    private readonly ITradingTransactionWriteDbContext _transactionWriteDbContext;

    public ThisControllerThing(ITradingTransactionWriteDbContext transactionWriteDbContext)
    {
        _transactionWriteDbContext = transactionWriteDbContext;
    }

    public void DoSomeDatabaseStuff()
    {
    }
}
