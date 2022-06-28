namespace AutomatedFinances.Application.TradingManager;

public interface ITradingCommandService
{
    Task<bool> AddTrade(string from, string to, DateTime occuredAt, CancellationToken ct);
}