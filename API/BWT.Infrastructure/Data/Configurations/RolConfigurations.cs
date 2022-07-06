using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class RolConfigurations : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdRol");

            builder.ToTable("tbRol");

            builder.Property(e => e.NameRol)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
