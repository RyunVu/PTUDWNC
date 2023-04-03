using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public class UnitDetail : IEntity
    {
        public int Id { get; set; }
        public string UnitTag { get; set; }
        public int Price { get; set; }

        // Số lượng tồn
        public int Quantity { get; set; }

        // % giảm giá
        public float Discount { get; set; }

        // Số lượng đã bán
        public int SoldCount { get; set; }

        public int ProductId { get; set; }

        // Properties
        public Product Product { get; set; }
    }
}
