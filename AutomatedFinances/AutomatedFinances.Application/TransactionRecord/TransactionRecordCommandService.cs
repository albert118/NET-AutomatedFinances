using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using Microsoft.Extensions.Logging;

namespace AutomatedFinances.Application.TransactionRecord;

[InstanceScopedService]
public class TransactionRecordCommandService : ITransactionRecordCommandService
{
    private readonly ILogger<TransactionRecordCommandService> _logger;
    private readonly ITransactionWriteDbContext _writeDbContext;

    public TransactionRecordCommandService(ILogger<TransactionRecordCommandService> logger, ITransactionWriteDbContext writeDbContext) {
        _logger = logger;
        _writeDbContext = writeDbContext;
    }

    public async Task<bool> AddTransaction(
        string description, long totalCost, DateTime transactionDate, string reference,
        CancellationToken ct
    ) {
        var recordedAtNow = DateTime.UtcNow;
        const string userName = "SYSTEM";

        _logger.LogInformation("Adding new {EntityName} at {RecordedAtDateTime} by user {UserName}",
            nameof(FinancialTransactionRecord),
            recordedAtNow,
            userName
        );

        await _writeDbContext.FinancialTransactionRecords.AddAsync(new() {
            Description = description,
            TotalCost = totalCost,
            OccuredAtDateTime = transactionDate,
            Reference = reference,
            RecordedAtDateTime = recordedAtNow,
            SavedBy = userName
        }, ct);

        await _writeDbContext.SaveChangesAsync(ct);

        return true;
    }
}
