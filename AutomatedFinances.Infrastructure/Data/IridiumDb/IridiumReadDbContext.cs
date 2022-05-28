using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure.Data.IridiumDb
{
    internal sealed class IridiumReadDbContext : IridiumWriteDbContext
    {
        public IridiumReadDbContext(DbContextOptions<IridiumReadDbContext> opts) : base(opts) {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default) {
            return Task.FromException<int>(
                new NotSupportedException(
                    $"{nameof(IridiumReadDbContext)} is a readonly context and cannot save changes!"));
        }
    }
}