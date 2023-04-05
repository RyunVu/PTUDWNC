using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Core.DTO
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Actived { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int UnitCount { get; set; }
        //public IList<UnitDetail> UnitDetails { get; set; }
        //public IList<OrderDetail> OrderDetails { get; set; }
    }
}
