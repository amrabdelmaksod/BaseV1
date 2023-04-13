using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class PodcastConfiguration : IEntityTypeConfiguration<Podcast>
    {
        public void Configure(EntityTypeBuilder<Podcast> builder)
        {
            builder.ToTable("Podcasts");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.AudioUrl).IsRequired().HasMaxLength(200);
            builder.Property(p => p.PublishDate).IsRequired();
            builder.Property(p => p.CreatedDate)
               .HasColumnType("datetime2")
               .IsRequired();
            builder.Property(p => p.Duration)
              .HasColumnType("time")
              .IsRequired();
        }
    }

}
