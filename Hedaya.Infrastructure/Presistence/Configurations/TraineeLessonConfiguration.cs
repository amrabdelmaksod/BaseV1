using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class TraineeLessonConfiguration : IEntityTypeConfiguration<TraineeLesson>
    {
        public void Configure(EntityTypeBuilder<TraineeLesson> builder)
        {
            builder.ToTable("TraineeLessons");
            builder.HasKey(tl => tl.Id);

            builder.HasOne(tl => tl.Trainee)
                .WithMany(t => t.TraineeLessons)
                .HasForeignKey(tl => tl.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tl => tl.Lesson)
                .WithMany()
                .HasForeignKey(tl => tl.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

           
        }
    }

}
