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
        }
    }
}
