using BWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BWT.Infrastructure.Data.Configurations
{
    public class PartnersConfigurations : IEntityTypeConfiguration<Partners>
    {
        public void Configure(EntityTypeBuilder<Partners> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("tbPartners");

            builder.Property(e => e.Id)
                .HasColumnName("IdPartners");

            builder.Property(e => e.DescriptionPartners)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsRequired(true);

            builder.HasOne(d => d.FkUserInfoNavigation)
                .WithMany(p => p.TbPartners)
                .HasForeignKey(d => d.FkUserInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbUserInfo_tbPartners");
        }
    }
}
