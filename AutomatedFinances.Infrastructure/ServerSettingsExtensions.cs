using AutomatedFinances.Infrastructure.Data;

namespace AutomatedFinances.Infrastructure
{
    internal static class ServerSettingsExtensions
    {
        public static DatabaseSettings ToDataBaseSettings(this ServerSettings serverSettings) =>
            new() {
                Server = serverSettings.IridiumServerPath,
                Password = serverSettings.AutomatedFinancesDbPassword,
                Database = serverSettings.AutomatedFinancesDbName,
                UserName = serverSettings.AutomatedFinancesDbUser
            };
    }
}