using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Application.TradingManager.Dtos;
using Microsoft.Extensions.Logging;

namespace AutomatedFinances.Application.TradingManager;

[InstanceScopedService]
public class TradingQueryService : ITradingQueryService
{
    private readonly ILogger<TradingQueryService> _logger;
    private readonly ITradingTransactionReadDbContext _transactionReadDbContext;

    public TradingQueryService(
        ILogger<TradingQueryService> logger,
        ITradingTransactionReadDbContext transactionReadDbContext)
    {
        _logger = logger;
        _transactionReadDbContext = transactionReadDbContext;
    }

    public IEnumerable<TradingInfo> GetAllTrades()
    {
        _logger.LogInformation("Getting all trade data");

        var transactions = _transactionReadDbContext.GenericTransactions.ToList();

        return transactions.Select(d => new TradingInfo(d.TrackingId, d.OccuredAtDateTime));
    }
}