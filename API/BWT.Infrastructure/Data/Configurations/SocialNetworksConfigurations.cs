using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class SocialNetworksConfigurations : IEntityTypeConfiguration<SocialNetworks>
    {
        public void Configure(EntityTypeBuilder<SocialNetworks> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_tbSocialNetwork");

            builder.ToTable("tbSocialNetworks");

            builder.Property(e => e.Id)
                .HasColumnName("IdSocialNetwork");

            builder.Property(e => e.Facebook)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Instragram)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Tiktok)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Twitch)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Twitter)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Youtube)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.HasOne(d => d.FkUserInfoNavigation)
                .WithMany(p => p.TbSocialNetworks)
                .HasForeignKey(d => d.FkUserInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbUserInfo_tbSocialNetwork");
        }
    }
}
