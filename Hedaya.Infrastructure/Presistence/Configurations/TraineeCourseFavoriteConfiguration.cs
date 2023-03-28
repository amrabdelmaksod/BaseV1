using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class TraineeCourseFavoriteConfiguration : IEntityTypeConfiguration<TraineeCourseFavorite>
    {
        public void Configure(EntityTypeBuilder<TraineeCourseFavorite> builder)
        {
            builder.ToTable("TraineeCourseFavorites");
            builder.HasKey(f => new { f.TraineeId, f.CourseId });

            builder.HasOne(f => f.Trainee)
                .WithMany(t => t.Favorites)
                .HasForeignKey(f => f.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Course)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
