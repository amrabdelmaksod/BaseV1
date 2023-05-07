using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class TraineeExplanationFavouriteConfiguration : IEntityTypeConfiguration<TraineeExplanationFavourite>
    {
        public void Configure(EntityTypeBuilder<TraineeExplanationFavourite> builder)
        {

            builder.ToTable("TraineeExplanationFavourites");
            builder.HasKey(x => new { x.TraineeId, x.MethodologicalExplanationId });

            builder.HasOne(x => x.Trainee)
                .WithMany(a=>a.TraineeExplanationFavourites)
                .HasForeignKey(x => x.TraineeId);

            builder.HasOne(x => x.MethodologicalExplanation)
                .WithMany(a=>a.TraineeExplanationFavourites)
                .HasForeignKey(x => x.MethodologicalExplanationId);
        }
    }

}
