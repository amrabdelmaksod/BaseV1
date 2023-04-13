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
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.HasQueryFilter(a => !a.Deleted);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }
}
