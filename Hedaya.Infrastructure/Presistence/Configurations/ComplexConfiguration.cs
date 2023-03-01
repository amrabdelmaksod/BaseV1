using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{
    public class ComplexConfiguration : IEntityTypeConfiguration<Complex>
    {
        public void Configure(EntityTypeBuilder<Complex> builder)
        {
            builder.ToTable("Complexes");
            builder.Property(a=>a.Terms).HasMaxLength(1000).IsRequired();
            builder.Property(a=>a.Conditions).HasMaxLength(2000).IsRequired();
        }
    }
}
