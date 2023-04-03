using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class TraineeFavouriteProgramConfiguration : IEntityTypeConfiguration<TraineeFavouriteProgram>
    {
        public void Configure(EntityTypeBuilder<TraineeFavouriteProgram> builder)
        {
            builder.ToTable("TraineeFavouritePrograms");

            builder.HasKey(e => new { e.TraineeId, e.TrainingProgramId });

            builder.HasOne(e => e.Trainee)
                .WithMany(e => e.FavouritePrograms)
                .HasForeignKey(e => e.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.TrainingProgram)
                .WithMany(e => e.TraineeFavouritePrograms)
                .HasForeignKey(e => e.TrainingProgramId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
