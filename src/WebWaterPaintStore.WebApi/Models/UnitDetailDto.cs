using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.WebApi.Models
{
    public class UnitDetailDto
    {
        public int Id { get; set; }
        public string UnitTag { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int SoldCount { get; set; }
        public bool Actived { get; set; }
        public int ProductId { get; set; }
    }
}
