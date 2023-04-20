using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Persistence.Configurations
{
    public class TrainingProgramNoteConfiguration : IEntityTypeConfiguration<TrainingProgramNote>
    {
        public void Configure(EntityTypeBuilder<TrainingProgramNote> builder)
        {
            builder.ToTable("TrainingProgramNotes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TextAr)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.TextEn)
                .HasMaxLength(255);

            builder.Property(x => x.SortIndex)
                .IsRequired();

            builder.HasOne(x => x.TrainingProgram)
                .WithMany(x => x.TrainingProgramNotes)
                .HasForeignKey(x => x.TrainingProgramId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
