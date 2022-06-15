using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Application.Interfaces
{
    public interface ITradingTransactionWriteDbContext : ITradingTransactionReadDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
        int SaveChanges();
    }
}