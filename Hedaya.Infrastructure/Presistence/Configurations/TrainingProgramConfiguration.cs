
using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Hedaya.Infrastructure.Presistence.Configurations
{


    public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            builder.ToTable("TrainingPrograms");

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Id)
                .ValueGeneratedOnAdd();

            builder.Property(tp => tp.TitleAr)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(tp => tp.TitleEn)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(tp => tp.Description).HasMaxLength(2000)
                .IsRequired();

            builder.Property(tp => tp.StartDate)
                .IsRequired();

            builder.Property(tp => tp.EndDate)
                .IsRequired();

            builder.Property(tp => tp.ImgUrl)
                .IsRequired();

            builder.HasMany(tp => tp.TraineeFavouritePrograms)
                .WithOne(tfp => tfp.TrainingProgram)
                .HasForeignKey(tfp => tfp.TrainingProgramId);

            builder.HasOne(tp => tp.SubCategory)
      .WithMany(sc => sc.TrainingPrograms)
      .HasForeignKey(tp => tp.SubCategoryId)
      .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tp => tp.Courses)
          .WithOne(c => c.TrainingProgram)
          .HasForeignKey(c => c.TrainingProgramId);


            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }

}
