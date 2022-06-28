namespace AutomatedFinances.Infrastructure;

internal static class Program
{
    // private static void Main()
    // {
    //     var container = BuildContainer();
    //
    //     using var scope = container.BeginLifetimeScope();
    //
    //     // var t = scope.Resolve<ThisControllerThing>();
    //     // t.DoSomeDatabaseStuff();
    // }

    // private static IContainer BuildContainer()
    // {
    //     var builder = new ContainerBuilder();
    //
    //     builder.RegisterType<TradingTransactionWriteDbContext>()
    //         .WithParameter("options", GetWriteDbContextOptions())
    //         .UsingConstructor(typeof(DbContextOptions<TradingTransactionWriteDbContext>))
    //         .As<ITradingTransactionWriteDbContext>()
    //         .InstancePerLifetimeScope();
    //
    //     // builder.RegisterType<ThisControllerThing>().AsSelf();
    //
    //     return builder.Build();
    // }
    //
    // private static DbContextOptions<TradingTransactionWriteDbContext> GetWriteDbContextOptions()
    // {
    //     var configuration = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         // .AddJsonFile(AppSettingsFilePath)
    //         .Build();
    //
    //     var builder = new DbContextOptionsBuilder<TradingTransactionWriteDbContext>()
    //         .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"));
    //
    //     return builder.Options;
    // }
}