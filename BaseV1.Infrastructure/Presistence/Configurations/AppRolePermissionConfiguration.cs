using BaseV1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseV1.Infrastructure.Presistence.Configurations
{
    public class AppRolePermissionConfiguration : IEntityTypeConfiguration<AppRolePermissions>
    {
        public void Configure(EntityTypeBuilder<AppRolePermissions> builder)
        {
            builder.ToTable(nameof(AppRolePermissions));


            builder.HasOne(a=>a.AppRole).WithMany(a=>a.AppRolePermissions).HasForeignKey(a=>a.RoleId);
            builder.HasOne(a=>a.AppPermission).WithMany(a=>a.AppRolePermissions).HasForeignKey(a=>a.PermissionId);


        }
    }
}
