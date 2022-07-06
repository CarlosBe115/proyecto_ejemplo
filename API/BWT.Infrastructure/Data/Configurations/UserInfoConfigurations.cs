using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class UserInfoConfigurations : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbUserInfo");

            builder.Property(e => e.Id)
                .HasColumnName("IdUserInfo");

            builder.Property(e => e.BirthDay).HasColumnType("date");

            builder.Property(e => e.FullNames)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false);

            builder.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ImageProfile)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LastNames)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false);

            builder.Property(e => e.NameTag)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.FkAccessNavigation)
                .WithMany(p => p.TbUserInfo)
                .HasForeignKey(d => d.FkAccess)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbAccess_tbUserInfo");
        }
    }
}
