using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new
            {
                od.OrderId,
                od.ProductId,
            });

            builder.ToTable("OrderDetails");
        }
    }

}
