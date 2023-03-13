using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SuggestionAndComplaintConfiguration : IEntityTypeConfiguration<SuggestionAndComplaint>
{
    public void Configure(EntityTypeBuilder<SuggestionAndComplaint> builder)
    {
        builder.ToTable("SuggestionAndComplaints");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.TraineeName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(50);
        builder.Property(e => e.Subject).HasMaxLength(100);
        builder.Property(e => e.Message).HasMaxLength(1000);
        builder.Property(e => e.CreationDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").IsRequired(); ;
    }
}
