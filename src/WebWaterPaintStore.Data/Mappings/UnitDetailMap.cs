using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class UnitDetailMap : IEntityTypeConfiguration<UnitDetail>
    {
        public void Configure(EntityTypeBuilder<UnitDetail> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("UnitDetails");

            builder.Property(s => s.UnitTag)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.Discount)
                .HasDefaultValue(0);

            builder.Property(s => s.SoldCount)
               .HasMaxLength(0);

            builder.Property(p => p.Actived)
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
