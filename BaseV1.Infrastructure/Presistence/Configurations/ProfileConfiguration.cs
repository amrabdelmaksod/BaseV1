using BaseV1.Domain.Entities.Authintication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseV1.Infrastructure.Presistence.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a=>a.JobTitle).HasMaxLength(256);
            builder.Property(a=>a.Twitter).HasMaxLength(256);
            builder.Property(a=>a.Facebook).HasMaxLength(256);
          
        
        }
    }
}
