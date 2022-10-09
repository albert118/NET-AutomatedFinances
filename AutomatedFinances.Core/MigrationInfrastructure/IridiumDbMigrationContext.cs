using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Core.MigrationInfrastructure;

public class IridiumDbMigrationContext : DbContext
{
    public IridiumDbMigrationContext(DbContextOptions opts) : base(opts)
    {
    }

    public DbSet<FinancialTransactionRecord> FinancialTransactionRecords => Set<FinancialTransactionRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new FinancialTransactionRecordConfig().Configure(builder.Entity<FinancialTransactionRecord>());
    }
}
