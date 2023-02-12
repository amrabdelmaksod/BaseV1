﻿using BaseV1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseV1.Infrastructure.Presistence.Configurations
{
    public class AppUserRolesConfiguration : IEntityTypeConfiguration<AppUserRoles>
    {
        public void Configure(EntityTypeBuilder<AppUserRoles> builder)
        {
           builder.ToTable(nameof(AppUserRoles));


            builder.HasOne(a => a.AppRole).WithMany(a => a.AppUserRoles).HasForeignKey(a => a.RoleId);
            builder.HasOne(a => a.AppUser).WithMany(a => a.AppUserRoles).HasForeignKey(a => a.AppUserId);

        }
    }
}
