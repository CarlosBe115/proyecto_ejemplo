using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class UserClanConfigurations : IEntityTypeConfiguration<UserClan>
    {
        public void Configure(EntityTypeBuilder<UserClan> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbUserClan");

            builder.Property("Id")
                .HasColumnName("IdUserClan");

            builder.Property(e => e.DateRegister).HasColumnType("date");

            builder.Property(e => e.FkUcrol)
                .HasColumnName("FkUCRol")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.FkClanNavigation)
                .WithMany(p => p.TbUserClan)
                .HasForeignKey(d => d.FkClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbClans_tbUserClan");

            builder.HasOne(d => d.FkUserNavigation)
                .WithMany(p => p.TbUserClan)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbUserInfo_tbUserClan");

            builder.HasOne(d => d.FkUcrolNavigation)
                .WithMany(p => p.TbUserClan)
                .HasForeignKey(d => d.FkUcrol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbUCRol_tbUserClan");
        }
    }
}
