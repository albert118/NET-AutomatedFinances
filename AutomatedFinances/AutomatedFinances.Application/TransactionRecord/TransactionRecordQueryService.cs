using AutomatedFinances.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AutomatedFinances.Application.TransactionRecord;

[InstanceScopedService]
public class TransactionQueryService : ITransactionQueryService
{
    private readonly ILogger<TransactionQueryService> _logger;
    private readonly ITransactionReadDbContext _transactionReadDbContext;

    public TransactionQueryService(ILogger<TransactionQueryService> logger, ITransactionReadDbContext transactionReadDbContext)
    {
        _logger = logger;
        _transactionReadDbContext = transactionReadDbContext;
    }

    public IEnumerable<Dtos.TransactionRecord> GetAllTransactionRecords()
    {
        var transactionRecords = _transactionReadDbContext.FinancialTransactionRecords.ToList();

        return transactionRecords.Select(txRecord => new Dtos.TransactionRecord(
            txRecord.Name,
            txRecord.TotalCost,
            txRecord.OccuredAtDateTime,
            txRecord.SavedAtDateTime
        ));
    }
}
