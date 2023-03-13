using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class PlatformWorkAxesConfiguration : IEntityTypeConfiguration<PlatformWorkAxes>
    {
        public void Configure(EntityTypeBuilder<PlatformWorkAxes> builder)
        {
           builder.ToTable("PlatformWorkAxes");
            builder.HasKey(x => x.Id);
            builder.Property(a=>a.Title).HasMaxLength(256).IsRequired();
            builder.Property(a=>a.Description).HasMaxLength(2000).IsRequired();
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);

         
        }
    }
}
