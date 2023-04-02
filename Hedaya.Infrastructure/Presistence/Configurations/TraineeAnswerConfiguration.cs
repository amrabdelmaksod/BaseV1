using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{


    public class TraineeAnswerConfiguration : IEntityTypeConfiguration<TraineeAnswer>
    {
        public void Configure(EntityTypeBuilder<TraineeAnswer> builder)
        {
            builder.HasKey(ta => ta.Id);

            builder.Property(ta => ta.SelectedAnswers)
                .IsRequired();

            builder.Property(ta => ta.Score)
                .IsRequired();

            builder.HasOne(ta => ta.Trainee)
                .WithMany(t => t.TraineeAnswers)
                .HasForeignKey(ta => ta.TraineeId);

            builder.HasOne(ta => ta.CourseTest)
                .WithMany(ct => ct.TraineeAnswers)
                .HasForeignKey(ta => ta.CourseTestId);

            builder.HasOne(ta => ta.Question)
                .WithMany(q => q.TraineeAnswers)
                .HasForeignKey(ta => ta.QuestionId);
        }
    }

}
