using Hedaya.Domain.Entities.Authintication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
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
