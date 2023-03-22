using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class ChatQuestionConfiguration : IEntityTypeConfiguration<ChatQuestions>
    {
        public void Configure(EntityTypeBuilder<ChatQuestions> builder)
        {
            builder.ToTable("ChatQuestions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
