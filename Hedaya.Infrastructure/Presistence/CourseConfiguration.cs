﻿using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence
{
   

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.TitleAr).HasMaxLength(256).IsRequired();
            builder.Property(c => c.TitleEn).HasMaxLength(256).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(2000).IsRequired();
            builder.Property(c => c.ImageUrl).HasMaxLength(200).IsRequired();
            builder.Property(c => c.CourseSyllabus).HasMaxLength(500).IsRequired();
            builder.Property(c => c.CourseFeatures).HasMaxLength(500).IsRequired();
            builder.Property(c => c.AboutCourse).HasMaxLength(500).IsRequired();
            builder.Property(c => c.StartDate).IsRequired();
            builder.Property(tp => tp.EndDate)
                .IsRequired();
            builder.Property(c => c.Duration).IsRequired();
            builder.Property(c => c.InstructorId).IsRequired();
            builder.Property(c => c.SubCategoryId)
           .IsRequired();
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);

            builder.HasOne(c => c.Instructor)
                   .WithMany(i => i.Courses)
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.SubCategory)
          .WithMany(sc => sc.Courses)
          .HasForeignKey(c => c.SubCategoryId)
          .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CourseTopics)
                   .WithOne(ct => ct.Course)
                   .HasForeignKey(ct => ct.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);




            builder.HasMany(c => c.CourseTests)
                  .WithOne(ct => ct.Course)
                  .HasForeignKey(ct => ct.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Forum)
             .WithOne(f => f.Course)
             .HasForeignKey<Forum>(f => f.CourseId)
             .IsRequired();

            builder.HasOne(c => c.TrainingProgram)
           .WithMany(tp => tp.Courses)
           .HasForeignKey(c => c.TrainingProgramId);
        }
    }

}
