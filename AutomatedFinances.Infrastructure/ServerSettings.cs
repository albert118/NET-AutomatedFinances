#pragma warning disable 8618
namespace AutomatedFinances.Infrastructure
{
    internal sealed class ServerSettings
    {
        public string IridiumServerPath { get; init; }

        public string AutomatedFinancesDbName { get; init; }

        public string AutomatedFinancesDbPassword { get; init; }
        
        public string AutomatedFinancesDbUser { get; init; }
    }
}
#pragma warning restore 8618
