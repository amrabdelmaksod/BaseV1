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

            builder.Property(me => me.Facebook)
             .HasMaxLength(200)
            ;
            builder.Property(me => me.Youtube)
         .HasMaxLength(200)
        ;
            builder.Property(me => me.Telegram)
        .HasMaxLength(200)
       ;

            builder.Property(me => me.Whatsapp)
    .HasMaxLength(15)
   ;

            builder.Property(me => me.ImageUrl).HasMaxLength(200);

            builder.HasOne(mc => mc.SubCategory)
                .WithMany(mc => mc.MassCultures)
                .HasForeignKey(mc => mc.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
