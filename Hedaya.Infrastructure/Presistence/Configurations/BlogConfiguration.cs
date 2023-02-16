﻿using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
            builder.HasKey(x => x.Id);
            builder.Property(a=>a.Title).HasMaxLength(500).IsRequired();
            builder.Property(a=>a.Description).HasMaxLength(2000);
            builder.Property(a=>a.ImagePath).HasMaxLength(200);
        }
    }
}
