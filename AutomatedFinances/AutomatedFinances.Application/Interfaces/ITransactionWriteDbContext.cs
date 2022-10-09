namespace AutomatedFinances.Application.Interfaces;

public interface ITransactionWriteDbContext : ITransactionReadDbContext
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}
