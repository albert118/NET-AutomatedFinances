namespace AutomatedFinances.Application.TransactionRecord;

public interface ITransactionRecordCommandService
{
    public Task<bool> AddTransaction(
        string description, long totalCost, DateTime transactionDate, string reference,
        CancellationToken ct
    );
}
