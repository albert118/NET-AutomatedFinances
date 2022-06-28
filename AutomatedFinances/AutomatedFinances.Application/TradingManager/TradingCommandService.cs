using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using Microsoft.Extensions.Logging;

namespace AutomatedFinances.Application.TradingManager;

[InstanceScopedService]
public class TradingCommandService : ITradingCommandService
{
    private readonly ILogger<TradingCommandService> _logger;
    private readonly ITradingTransactionWriteDbContext _tradingTransactionWriteDbContext;

    public TradingCommandService(
        ILogger<TradingCommandService> logger,
        ITradingTransactionWriteDbContext tradingTransactionWriteDbContext)
    {
        _logger = logger;
        _tradingTransactionWriteDbContext = tradingTransactionWriteDbContext;
    }

    public async Task<bool> AddTrade(string from, string to, DateTime occuredAt, CancellationToken ct)
    {
        _logger.LogInformation("Adding new Generic Transaction entity");

        await _tradingTransactionWriteDbContext.GenericTransactions.AddAsync(new GenericTransaction
        {
            From = from,
            To = to,
            OccuredAtDateTime = occuredAt,
            RecordedAtDateTime = DateTime.UtcNow,
            SavedBy = "THE SYSTEM"
        }, ct);

        await _tradingTransactionWriteDbContext.SaveChangesAsync(ct);

        _logger.LogInformation("Added Generic Transaction entity with 'smth'");

        return true;
    }
}
