using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Application.Interfaces
{
    public interface IIridiumWriteDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}