using Hedaya.Domain.Entities.Authintication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class AppUserRolesConfiguration : IEntityTypeConfiguration<AppUserRoles>
    {
        public void Configure(EntityTypeBuilder<AppUserRoles> builder)
        {
           builder.ToTable(nameof(AppUserRoles));

            builder.HasKey(a => new {a.AppUserId, a.RoleId});

            builder.HasOne(a => a.AppRole).WithMany(a => a.AppUserRoles).HasForeignKey(a => a.RoleId);
            builder.HasOne(a => a.AppUser).WithMany(a => a.AppUserRoles).HasForeignKey(a => a.AppUserId);

        }
    }
}
