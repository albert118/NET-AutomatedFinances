using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Application.Interfaces.Expenditures
{
    public interface IExpenditureWriteDbContext : IExpenditureReadDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
