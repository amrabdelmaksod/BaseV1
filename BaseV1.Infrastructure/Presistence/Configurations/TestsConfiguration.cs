using BaseV1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseV1.Infrastructure.Presistence.Configurations
{
    public class TestsConfiguration : IEntityTypeConfiguration<TestClass>
    {
        public void Configure(EntityTypeBuilder<TestClass> builder)
        {
            builder.ToTable("Tests");

            builder.HasKey(x => x.Id);
            builder.Property(a=>a.Name).HasMaxLength(256).IsRequired();
            
        }
    }
}
