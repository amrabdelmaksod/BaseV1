
using Hedaya.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hedaya.Infrastructure.Presistence.Configurations
{


    public class ExplanationVideoConfiguration : IEntityTypeConfiguration<ExplanationVideo>
    {
        public void Configure(EntityTypeBuilder<ExplanationVideo> builder)
        {
            builder.ToTable("ExplanationVideos"); // set the table name

            builder.HasKey(ev => ev.Id); // set the primary key

            builder.Property(ev => ev.VideoUrl)
                   .IsRequired()
                   .HasMaxLength(200); // set the VideoUrl property constraints

            builder.Property(ev => ev.TitleAr)
                   .IsRequired()
                   .HasMaxLength(100); // set the TitleAr property constraints

            builder.Property(ev => ev.Duration)
                   .IsRequired(); // set the Duration property constraints



            builder.Property(ev => ev.TitleEn)
                   .IsRequired()
                   .HasMaxLength(100); // set the TitleEn property constraints

            builder.Property(ev => ev.Description)
                   .HasMaxLength(500); // set the Description property constraints

            builder.HasOne(ev => ev.MethodologicalExplanation)
                   .WithMany(me => me.ExplanationVideos)
                   .HasForeignKey(ev => ev.MethodologicalExplanationId); // configure the navigation property

            // add any other configurations you need
        }
    }

}
