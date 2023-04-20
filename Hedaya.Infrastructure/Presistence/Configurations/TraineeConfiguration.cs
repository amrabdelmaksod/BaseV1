using Hedaya.Domain.Entities;
using Hedaya.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class TraineeConfiguration : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.ToTable("Trainees");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(50);
            builder.Property(a => a.FullName).HasMaxLength(256).IsRequired();


            builder.Property(u => u.JobTitle)
        .HasMaxLength(50);

            builder.Property(u => u.ProfilePictureImagePath)
     .HasMaxLength(200);

            builder.Property(u => u.Facebook)
                .HasMaxLength(100);

            builder.Property(u => u.Twitter)
                .HasMaxLength(100);

            builder.Property(u => u.Whatsapp)
                .HasMaxLength(20);
            builder.HasQueryFilter(a => !a.Deleted);

            builder.Property(u => u.Telegram)
                .HasMaxLength(100);
            builder.Property(u => u.EducationalDegree)
             .HasConversion<int>()
             .HasDefaultValue(EducationalDegree.None);

            builder.HasMany(e => e.FavouritePrograms)
       .WithOne()
       .HasForeignKey(e => e.TraineeId)
       .OnDelete(DeleteBehavior.Cascade);


            builder.Property(a=>a.AppUserId).HasMaxLength(450).IsRequired();
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }
}
