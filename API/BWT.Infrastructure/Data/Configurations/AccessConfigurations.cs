using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class AccessConfigurations : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbAccess");

            builder.Property(e => e.Id)
                .HasColumnName("IdAccess");

            builder.Property(e => e.AccessKey)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false);

            builder.Property(e => e.EmailPassword)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.TimeBan).HasColumnType("date");

            builder.HasOne(d => d.FkRolNavigation)
                .WithMany(p => p.TbAccess)
                .HasForeignKey(d => d.FkRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbRol_tbUserInfo");
        }
    }
}
