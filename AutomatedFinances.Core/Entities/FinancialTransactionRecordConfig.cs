using AutomatedFinances.Core.Globalisation.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Core.Entities;

public class FinancialTransactionRecordConfig
{
    public void Configure(EntityTypeBuilder<FinancialTransactionRecord> builder) {
        builder.ToTable(nameof(FinancialTransactionRecord));

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasValueGenerator<NewIdGenerator>();

        builder.Property(e => e.SavedAtDateTime)
            .HasValueGenerator<UtcNowGenerator>();

        builder.Ignore(e => e.Name);
    }
}
