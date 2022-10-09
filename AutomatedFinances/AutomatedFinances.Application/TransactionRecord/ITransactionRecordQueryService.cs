namespace AutomatedFinances.Application.TransactionRecord;

public interface ITransactionQueryService
{
    public IEnumerable<Dtos.TransactionRecord> GetAllTransactionRecords();
}
