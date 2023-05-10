using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.ToTable("PostLikes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.PostId)
                .IsRequired();

            builder.HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.TraineeId)
                .IsRequired();

            builder.HasOne(l => l.Trainee)
                .WithMany()
                .HasForeignKey(l => l.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
