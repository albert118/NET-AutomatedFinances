namespace AutomatedFinances.Infrastructure
{
    internal sealed class ServerSettings
    {
        public string IridiumServerPath { get; set; }

        public string AutomedFinancesDbName { get; set; }

        public string AutomedFinancesDbPassword { get; set; }

        public string AutomedFinancesDbUser { get; set; }
    }
}
