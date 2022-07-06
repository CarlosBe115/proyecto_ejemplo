using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class GamesConfigurations : IEntityTypeConfiguration<Games>
    {
        public void Configure(EntityTypeBuilder<Games> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbGames");

            builder.Property(e => e.Id)
                .HasColumnName("IdGames");

            builder.Property(e => e.DescriptionGame)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ImageGame)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.NameGame)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
