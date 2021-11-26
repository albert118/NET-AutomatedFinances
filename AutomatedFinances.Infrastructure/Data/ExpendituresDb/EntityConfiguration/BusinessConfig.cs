using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb.EntityConfiguration
{
    internal class BusinessConfig : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.BusinessName)
                .HasMaxLength(255);
        }
    }
}
