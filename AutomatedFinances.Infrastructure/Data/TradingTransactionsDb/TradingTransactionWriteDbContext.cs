using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;

public class TradingTransactionWriteDbContext : AsyncDbContext, ITradingTransactionWriteDbContext
{
    public TradingTransactionWriteDbContext(DbContextOptions<TradingTransactionWriteDbContext> options) : base(options)
    {
    }

    public TradingTransactionWriteDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<GenericTransaction> GenericTransactions => Set<GenericTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new GenericTransactionConfig().Configure(builder.Entity<GenericTransaction>());

        // base.OnModelCreating(builder);
        //
        // var contextReflection = typeof(TradingTransactionWriteDbContext);
        // var contextNamespace = contextReflection.Namespace;
        //
        // if (contextNamespace == null) {
        //     throw new InvalidOperationException(
        //         $"Couldn't determine the namespace of {nameof(TradingTransactionWriteDbContext)}, please validate this context and it's data sets are correctly namespaced");
        // }
        //
        // builder.ApplyConfigurationsFromAssembly(
        //     contextReflection.Assembly,
        //     type => type.Namespace?.StartsWith(contextNamespace) ?? false);
    }
}