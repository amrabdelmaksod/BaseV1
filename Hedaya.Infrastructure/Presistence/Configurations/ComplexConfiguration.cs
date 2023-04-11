using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class ComplexConfiguration : IEntityTypeConfiguration<Complex>
    {
        public void Configure(EntityTypeBuilder<Complex> builder)
        {
            builder.ToTable("Complexes");
          
            builder.HasKey(c => c.Id);
            builder.Property(c => c.TitleAr).HasMaxLength(100).IsRequired();
            builder.Property(c => c.AddressDescription).HasMaxLength(500);
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Mobile).HasMaxLength(20);
            builder.Property(c => c.LandlinePhone).HasMaxLength(20);
            builder.Property(c => c.TermsAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.TermsEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.ConditionsAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.ConditionsEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.LogFilesAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.LogFilesEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.CookiesAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.CookiesEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.VisionAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.VisionEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.MissionAr).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.MissionEn).HasMaxLength(5000).IsRequired();
            builder.Property(c => c.AboutPlatformVideoUrl).HasMaxLength(200);
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }
}
