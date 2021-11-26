using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb.EntityConfiguration
{
    internal class NoteConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.Id);
            
            // TODO: might not be needed as it's already defined in CostConfig.cs
            builder.HasMany(e => e.Cost)
                .WithMany(e => e.Note);
        }
    }
}
