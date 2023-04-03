using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ImgIconUrl).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.MainCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.MainCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(sc => sc.TrainingPrograms)
         .WithOne(tp => tp.SubCategory)
         .HasForeignKey(tp => tp.SubCategoryId)
         .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
