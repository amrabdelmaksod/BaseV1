using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Text).HasMaxLength(1000)
                .IsRequired();

            builder.Property(a => a.IsCorrect)
                .IsRequired();

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}
