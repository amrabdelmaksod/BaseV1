using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class ExplanationNoteConfiguration : IEntityTypeConfiguration<ExplanationNote>
    {
        public void Configure(EntityTypeBuilder<ExplanationNote> builder)
        {
            builder.ToTable("ExplanationNotes");

            builder.HasKey(en => en.Id);

            builder.Property(en => en.TitleAr)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(en => en.TitleEn)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(en => en.IconUrl)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(en => en.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasOne(en => en.MethodologicalExplanation)
                .WithMany(me => me.ExplanationNotes)
                .HasForeignKey(en => en.MethodologicalExplanationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
