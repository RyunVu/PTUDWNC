using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>{
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Orders");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.OrderDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(a => a.ShipName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(a => a.ShipAddress)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(a => a.Email)
              .IsRequired()
              .HasMaxLength(128);

            builder.Property(a => a.ShipTel)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            builder.HasMany(o => o.OrderDetails)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Orders_OrdersDetails")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

        }
    }
}
