using Hedaya.Domain.Entities;
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
            builder.Property(a=>a.Facebook).HasMaxLength(200);
            builder.Property(a=>a.Twitter).HasMaxLength(200);
            builder.Property(a=>a.Youtube).HasMaxLength(200);
            builder.Property(a=>a.Instagram).HasMaxLength(200);
            builder.Property(a=>a.Whatsapp).HasMaxLength(200);
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();

            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");

            builder.Property(a => a.Deleted).HasDefaultValue(false);
        }
    }
}
