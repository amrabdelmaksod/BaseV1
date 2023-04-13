using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
    
            builder.Property(a=>a.SecurityCode).HasMaxLength(10);
            builder.Property(a => a.FullName).HasMaxLength(256);

            builder.Property(u => u.DateOfBirth)
                .IsRequired()
                .HasDefaultValue(new DateTime(1997, 1, 1));
            builder.Property(u => u.Gender)
            .IsRequired()
            .HasDefaultValue(Gender.Unknown);
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.IsActive).HasDefaultValue(true);
            builder.HasQueryFilter(a => !a.Deleted);

            builder.HasMany(u => u.Notifications)
          .WithOne(n => n.AppUser)
          .HasForeignKey(n => n.AppUserId)
          .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
