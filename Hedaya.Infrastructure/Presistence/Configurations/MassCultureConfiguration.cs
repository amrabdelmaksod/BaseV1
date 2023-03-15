using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class MassCultureConfiguration : IEntityTypeConfiguration<MassCulture>
    {
        public void Configure(EntityTypeBuilder<MassCulture> builder)
        {
            builder.ToTable("MassCultures");

            builder.HasKey(mc => mc.Id);
            builder.Property(mc => mc.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(mc => mc.TitleAr).HasMaxLength(100).IsRequired();
            builder.Property(mc => mc.TitleEn).HasMaxLength(100).IsRequired();
            builder.Property(mc => mc.Description).HasMaxLength(1000).IsRequired();
             builder.Property(mc => mc.Duration)
            .HasDefaultValue(TimeSpan.FromSeconds(0));

            builder.HasOne(mc => mc.SubCategory)
                .WithMany(mc => mc.MassCultures)
                .HasForeignKey(mc => mc.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
