
using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Hedaya.Infrastructure.Presistence.Configurations
{
   

    public class TutorialConfiguration : IEntityTypeConfiguration<Tutorial>
    {
        public void Configure(EntityTypeBuilder<Tutorial> builder)
        {
            builder.ToTable("Tutorials");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasOne(t => t.TeachingStaff)
                .WithMany(t => t.Tutorials)
                .HasForeignKey(t => t.TeachingStaffId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
