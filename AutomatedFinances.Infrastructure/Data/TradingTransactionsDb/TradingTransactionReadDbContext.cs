using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;

public sealed class TradingTransactionReadDbContext : TradingTransactionWriteDbContext
{
    public TradingTransactionReadDbContext(DbContextOptions<TradingTransactionReadDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
    {
        return Task.FromException<int>(
            new NotSupportedException(
                $"{nameof(TradingTransactionReadDbContext)} is a readonly context and cannot save changes!"));
    }
}
