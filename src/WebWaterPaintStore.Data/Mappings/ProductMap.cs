using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(s => s.ShortDescription)
                .IsRequired()
                .HasMaxLength(5120);

            builder.Property(s => s.Meta)
                .IsRequired()
                .HasMaxLength(1280);

            builder.Property(s => s.UrlSlug)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Actived)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(o => o.CreatedDate)
            .HasColumnType("datetime");

            builder.Property(s => s.ImageUrl)
                .IsRequired()
                .HasMaxLength(512);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Products_Categories")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderDetails)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Products_OrderDetails")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.UnitDetails)
                .WithOne(p => p.Product)
                .HasForeignKey(s => s.ProductId)
                .HasConstraintName("FK_Products_UnitDetails")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
