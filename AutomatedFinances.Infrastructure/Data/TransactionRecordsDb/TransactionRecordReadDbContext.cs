using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure.Data.TransactionRecordsDb;

public sealed class TransactionReadDbContext : TransactionWriteDbContext
{
    public TransactionReadDbContext(DbContextOptions<TransactionReadDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
    {
        return Task.FromException<int>(
            new NotSupportedException(
                $"{nameof(TransactionReadDbContext)} is a readonly context and cannot save changes!"));
    }
}
