namespace AutomatedFinances.Application.Interfaces;

public interface ITradingTransactionWriteDbContext : ITradingTransactionReadDbContext
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}
