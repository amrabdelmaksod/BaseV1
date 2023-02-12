using BaseV1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseV1.Infrastructure.Presistence.Configurations
{
    public class AppPermissionConfiguration : IEntityTypeConfiguration<AppPermission>
    {
        public void Configure(EntityTypeBuilder<AppPermission> builder)
        {
           builder.ToTable("AppPermissions");
            builder.Property(a=>a.Name).HasMaxLength(256).IsRequired();
        }
    }
}
