using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class GentlemenScholarConfiguration : IEntityTypeConfiguration<GentlemenScholar>
    {
        public void Configure(EntityTypeBuilder<GentlemenScholar> builder)
        {
            builder.ToTable("GentlemenScholars");
            builder.Property(a=>a.Name).HasMaxLength(250).IsRequired();
            builder.Property(a=>a.Title).HasMaxLength(500);
            builder.Property(a=>a.Description).HasMaxLength(2000);
            builder.Property(a=>a.Facebook).HasMaxLength(200);
            builder.Property(a=>a.Twitter).HasMaxLength(200);
            builder.Property(a=>a.Youtube).HasMaxLength(200);
        }
    }
}
