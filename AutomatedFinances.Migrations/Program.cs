using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutomatedFinances.Migrations
{
    public static class Program
    {
        const bool RollbackLastChange = false;
        // TODO: Move this to ServerSettings!
        const string DbConn = "Server=192.168.1.134;Database=AutomatedFinancesDb;User Id=sa;Password=p9kNCTHi@91a;";
        
        private static void Main() {
            var serviceProvider = BuildServiceProvider(DbConn);

            using var scope = serviceProvider.CreateScope();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // TODO: pipe a console arg
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse'
            // ReSharper disable once HeuristicUnreachableCode
            if (RollbackLastChange) {
                RollBackLastMigration(runner);
            }
            
            UpdateDatabase(runner);
        }

        private static IServiceProvider BuildServiceProvider(string dbConn) {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(dbConn)
                    // Define the assembly containing the migrations
                    // TODO: better way to register migrations that this manual shit
                    .ScanIn(typeof(AddFinanceDomainTables).Assembly).For.Migrations()
                    .ScanIn(typeof(InsertPaymentMethods).Assembly).For.Migrations()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IMigrationRunner runner) => runner.MigrateUp();

        private static void RollBackLastMigration(IMigrationRunner runner) => runner.Rollback(1);
    }
}
