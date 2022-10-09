using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Infrastructure.Data.TransactionRecordsDb;

public class TransactionWriteDbContext : AsyncDbContext, ITransactionWriteDbContext
{
    public TransactionWriteDbContext(DbContextOptions<TransactionWriteDbContext> options) : base(options) { }

    public TransactionWriteDbContext(DbContextOptions options) : base(options) { }

    public DbSet<FinancialTransactionRecord> FinancialTransactionRecords => Set<FinancialTransactionRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new FinancialTransactionRecordConfig().Configure(builder.Entity<FinancialTransactionRecord>());
    }
}
