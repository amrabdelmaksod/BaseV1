using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text).HasMaxLength(1000)
                .IsRequired();

            builder.Property(q => q.Type)
                .IsRequired();

            builder.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}
