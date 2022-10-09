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

        var connectionString = configuration.GetConnectionString("IridiumDbConnection");

        Console.WriteLine(Directory.GetCurrentDirectory().ToString());

        var majorVersion = int.Parse(configuration.GetSection("ConnectionStrings:ServerVersionMajor").Value);
        var minorVersion = int.Parse(configuration.GetSection("ConnectionStrings:ServerVersionMinor").Value);
        var buildVersion = int.Parse(configuration.GetSection("ConnectionStrings:ServerVersionBuild").Value);

        var serverVersion = new MySqlServerVersion(new Version(majorVersion, minorVersion, buildVersion));

        var dbContextBuilder = new DbContextOptionsBuilder<IridiumDbMigrationContext>()
            .UseMySql(connectionString, serverVersion);

        return new(dbContextBuilder.Options);
    }
}
