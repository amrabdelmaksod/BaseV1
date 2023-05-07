using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class PodcastFavouriteConfiguration : IEntityTypeConfiguration<PodcastFavourite>
    {
        public void Configure(EntityTypeBuilder<PodcastFavourite> builder)
        {
            builder.ToTable("PodcastFavourites");
            builder.HasKey(pf => new { pf.TraineeId, pf.PodcastId });

            builder.HasOne(pf => pf.Trainee)
                   .WithMany(t => t.PodcastFavourites)
                   .HasForeignKey(pf => pf.TraineeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pf => pf.Podcast)
                   .WithMany(p => p.PodcastFavourites)
                   .HasForeignKey(pf => pf.PodcastId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
