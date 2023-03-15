
using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Hedaya.Infrastructure.Presistence.Configurations
{


    public class TeachingStaffConfiguration : IEntityTypeConfiguration<TeachingStaff>
    {
        public void Configure(EntityTypeBuilder<TeachingStaff> builder)
        {
            builder.ToTable("TeachingStaff");
            builder.HasKey(t => t.Id);


            builder.Property(t => t.Id).HasMaxLength(50);

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t => t.Facebook)
                .HasMaxLength(200);

            builder.Property(t => t.Twitter)
                .HasMaxLength(200);

            builder.Property(t => t.Youtube)
                .HasMaxLength(200);

            builder.HasMany(t => t.Tutorials)
                .WithOne(t => t.TeachingStaff)
                .HasForeignKey(t => t.TeachingStaffId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
