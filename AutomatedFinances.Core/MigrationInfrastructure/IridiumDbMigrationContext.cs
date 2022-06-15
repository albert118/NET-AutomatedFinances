using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Core.MigrationInfrastructure;

public class IridiumDbMigrationContext : DbContext
{
    public IridiumDbMigrationContext(DbContextOptions opts) : base(opts)
    {
    }

    public DbSet<GenericTransaction> GenericTransactions => Set<GenericTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new GenericTransactionConfig().Configure(builder.Entity<GenericTransaction>());
    }
}