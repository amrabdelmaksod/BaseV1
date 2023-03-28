using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class ForumConfiguration : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.HasMany(f => f.Posts)
                .WithOne(p => p.Forum)
                .HasForeignKey(p => p.ForumId)
                .IsRequired();
        }
    }
}
