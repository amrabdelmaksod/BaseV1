using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
            builder.HasKey(f => new { f.TraineeId, f.FriendId });

            builder.HasOne(f => f.Trainee)
                   .WithMany(t => t.Friends)
                   .HasForeignKey(f => f.TraineeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Friend)
                   .WithMany()
                   .HasForeignKey(f => f.FriendId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.RequestDate)
                   .IsRequired();

            builder.Property(f => f.AcceptedDate)
                   .IsRequired(false);

            builder.Property(f => f.Status)
                   .IsRequired();
        }
}

}
