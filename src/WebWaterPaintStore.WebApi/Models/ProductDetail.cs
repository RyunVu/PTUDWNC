using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.WebApi.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Meta { get; set; }

        public string UrlSlug { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool Actived { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        // Chi tiết hóa đơn
        //public virtual IList<OrderDetail> OrderDetails { get; set; }

        public IList<UnitDetailDto> UnitDetails { get; set; }
    }
}
