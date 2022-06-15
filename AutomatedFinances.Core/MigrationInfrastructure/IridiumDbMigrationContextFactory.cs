using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AutomatedFinances.Core.MigrationInfrastructure;

public class IridiumDbMigrationContextFactory : IDesignTimeDbContextFactory<IridiumDbMigrationContext>
{
    // Holds migration infrastructure settings
    private const string AppSettingsFilePath = "appsettings.json";

    public IridiumDbMigrationContext CreateDbContext(string[] args)
    {
        // configuration for a JSON settings file will not work without
        // this package installed 'Microsoft.Extensions.Configuration.Json'

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFilePath)
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder<IridiumDbMigrationContext>()
            .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"));

        return new(dbContextBuilder.Options);
    }
}