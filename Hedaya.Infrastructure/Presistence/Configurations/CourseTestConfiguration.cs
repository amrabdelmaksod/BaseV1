using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class CourseTestConfiguration : IEntityTypeConfiguration<CourseTest>
    {
        public void Configure(EntityTypeBuilder<CourseTest> builder)
        {
            builder.ToTable("CourseTests");
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Title).HasMaxLength(500)
                .IsRequired();

            builder.HasMany(ct => ct.Questions)
                .WithOne(q => q.CourseTest)
                .HasForeignKey(q => q.CourseTestId);

            builder.HasOne(c => c.Course)
                 .WithMany(c => c.CourseTests)
                 .HasForeignKey(c => c.CourseId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
