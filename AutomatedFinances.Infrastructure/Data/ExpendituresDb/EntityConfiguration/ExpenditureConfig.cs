using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb.EntityConfiguration
{
    internal class ExpenditureConfig : IEntityTypeConfiguration<Expenditure>
    {
        public void Configure(EntityTypeBuilder<Expenditure> builder)
        {
            builder.HasKey(e => e.Id);

            // builder.Property(e => e.Id)
            //    .HasValueGenerator<>();

            builder.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasConversion<string>();
        }
    }
}
