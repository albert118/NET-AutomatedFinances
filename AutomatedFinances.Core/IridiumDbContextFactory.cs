using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomatedFinances.Core
{
    public class IridiumDbContextFactory : IDesignTimeDbContextFactory<IridiumDbContext>
    {
        private const string AppSettingsFilePath = "appsettings.json";

        public IridiumDbContext CreateDbContext(string[] args) {
            // configuration for a JSON settings file will not work without this package installed
            // Microsoft.Extensions.Configuration.Json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsFilePath)
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<IridiumDbContext>()
                .UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"));

            return new IridiumDbContext(dbContextBuilder.Options);
        }
    }
}