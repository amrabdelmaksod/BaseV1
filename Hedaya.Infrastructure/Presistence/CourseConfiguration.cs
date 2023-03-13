using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.Property(a => a.Id).HasMaxLength(50).IsRequired();
            builder.Property(a=>a.Title).HasMaxLength(265).IsRequired();
            builder.Property(a=>a.InstructorId).HasMaxLength(50).IsRequired();         
        }
    }
}
