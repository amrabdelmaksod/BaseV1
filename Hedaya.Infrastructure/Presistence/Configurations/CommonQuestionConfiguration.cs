using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class CommonQuestionConfiguration : IEntityTypeConfiguration<CommonQuestion>
    {
        public void Configure(EntityTypeBuilder<CommonQuestion> builder)
        {
            builder.ToTable("CommonQuestions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Response)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
