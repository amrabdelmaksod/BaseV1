using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
   

    public class MethodologicalExplanationConfiguration : IEntityTypeConfiguration<MethodologicalExplanation>
    {
        public void Configure(EntityTypeBuilder<MethodologicalExplanation> builder)
        {
            builder.ToTable("MethodologicalExplanations");

            builder.HasKey(me => me.Id);

            builder.Property(me => me.TitleAr)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(me => me.TitleEn)
                .HasMaxLength(100)
                .IsRequired();

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

            builder.Property(me => me.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(mc => mc.Duration)
              .HasDefaultValue(TimeSpan.FromSeconds(0));

            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);

            builder.HasOne(me => me.SubCategory)
                .WithMany(sc => sc.MethodologicalExplanations)
                .HasForeignKey(me => me.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

     
        }
    }

}
