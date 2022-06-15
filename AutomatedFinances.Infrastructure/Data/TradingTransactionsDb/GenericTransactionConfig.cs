using AutomatedFinances.Core.Entities;
using AutomatedFinances.Infrastructure.Globalisation.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Infrastructure.Data.TradingTransactionsDb;

public class GenericTransactionConfig : IEntityTypeConfiguration<GenericTransaction>
{
    public void Configure(EntityTypeBuilder<GenericTransaction> builder)
    {
        builder.ToTable("GenericTransaction");

        builder.HasKey(e => e.TrackingId);

        builder.Property(e => e.TrackingId)
            .HasValueGenerator<NewIdGenerator>();

        builder.Property(e => e.SavedAtDateTime)
            .HasValueGenerator<UtcNowGenerator>();
    }
}
