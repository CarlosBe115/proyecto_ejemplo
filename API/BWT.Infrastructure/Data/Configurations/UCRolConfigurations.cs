using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class UCRolConfigurations : IEntityTypeConfiguration<UCRol>
    {
        public void Configure(EntityTypeBuilder<UCRol> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbUCRol");

            builder.Property(e => e.Id).HasColumnName("IdUCRol");

            builder.Property(e => e.NameRol)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
