using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Id).HasMaxLength(50);
            builder.Property(a=>a.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(a=>a.LastName) .HasMaxLength(128).IsRequired();
            builder.Property(a=>a.AppUserId).HasMaxLength(450).IsRequired();
            builder.Property(a => a.Deleted).HasDefaultValue(false);
            builder.Property(a => a.CreatedById).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
            builder.Property(a => a.ModifiedById).HasMaxLength(50);
        }
    }
}
