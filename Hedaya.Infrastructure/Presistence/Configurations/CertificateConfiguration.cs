using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificates");
            builder.HasKey(a => a.Id);
            builder.Property(a=>a.CourseId).HasMaxLength(50).IsRequired();
            builder.Property(a=>a.TraineeId).HasMaxLength(50).IsRequired();
            builder.Property(a=>a.CourseId).HasMaxLength(50).IsRequired();
            
            builder.Property(c => c.ImageUrl).HasMaxLength(200)
                .IsRequired();

       
            builder.Property(c => c.CertificateType)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }
}
