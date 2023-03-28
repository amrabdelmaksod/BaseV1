using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Text).HasMaxLength(2000).IsRequired();
            builder.Property(c => c.ImagePath).HasMaxLength(200).IsRequired(false);

            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PostId);

            builder.HasOne(c => c.Trainee)
                   .WithMany(t => t.Comments)
                   .HasForeignKey(c => c.TraineeId);
        }
    }
}
