using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class ClansConfigurations : IEntityTypeConfiguration<Clans>
    {
        public void Configure(EntityTypeBuilder<Clans> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbClans");

            builder.Property(e => e.Id)
                .HasColumnName("IdClan");

            builder.Property(e => e.FkUserCreator)
                .HasColumnName("FkUserCreator");

            builder.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.CreationClan).HasColumnType("date");

            builder.Property(e => e.DescriptionClan)
                .IsRequired()
                .HasMaxLength(225)
                .IsUnicode(false);

            builder.Property(e => e.NameClan)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Property(e => e.Ranked)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.FkGamesNavigation)
                .WithMany(p => p.TbClans)
                .HasForeignKey(d => d.FkGames)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbGames_tbClans");
        }
    }
}
