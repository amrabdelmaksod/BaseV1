﻿using Hedaya.Domain.Entities.Authintication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");
            builder.Property(a=>a.Name).HasMaxLength(256).IsRequired();
          
        }
    }
}