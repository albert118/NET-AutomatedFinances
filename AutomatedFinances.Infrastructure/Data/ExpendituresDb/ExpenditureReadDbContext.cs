using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb
{
    internal class ExpenditureReadDbContext : ExpenditureWriteDbContext
    {
        public ExpenditureReadDbContext(
            DbContextOptions<ExpenditureReadDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // AsyncDbContext funnaels all overloads into here.
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
        {
            return Task.FromException<int>(new NotSupportedException("This is a readonly context!"));
        }
    }
}
