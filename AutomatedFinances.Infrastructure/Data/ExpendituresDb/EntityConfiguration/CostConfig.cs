using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb.EntityConfiguration
{
    internal class CostConfig : IEntityTypeConfiguration<Cost>
    {
        public void Configure(EntityTypeBuilder<Cost> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Category)
                .HasMaxLength(30)
                .HasConversion<string>();
            builder.HasOne(e => e.Business);
            builder.HasOne(e => e.PaymentMethod);
            builder.HasOne(e => e.Note)
                .WithMany(e => e.Costs);
        }
    }
}
