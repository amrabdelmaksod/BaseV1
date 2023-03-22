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
            builder.Property(c => c.Title).HasMaxLength(100).IsRequired();
            builder.Property(c => c.AddressDescription).HasMaxLength(500);
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Mobile).HasMaxLength(20);
            builder.Property(c => c.LandlinePhone).HasMaxLength(20);
            builder.Property(c => c.Terms).HasMaxLength(5000);
            builder.Property(c => c.Conditions).HasMaxLength(5000);
            builder.Property(c => c.LogFiles).HasMaxLength(5000);
            builder.Property(c => c.Cookies).HasMaxLength(5000);
            builder.Property(c => c.Vision).HasMaxLength(5000);
            builder.Property(c => c.Mission).HasMaxLength(5000);
            builder.Property(c => c.AboutPlatformVideoUrl).HasMaxLength(200);
        }
    }
}
