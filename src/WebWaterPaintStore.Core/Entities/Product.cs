using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public class Product : IEntity{
        // Mã sản phẩm
        public int Id { get; set; }

        // Tên sản phẩm
        public string Name { get; set;}

        // Mô tả về sản phẩm
        public string ShortDescription { get; set; }

        // Metadata 
        public string Meta { get; set; }

        // Tên định danh 
        public string UrlSlug { get; set;}


        // Hiển thị trên menu
        public bool Actived { get; set; }

        // Ảnh sản phẩm
        public string ImageUrl { get; set; }


        // Loại của sản phẩm
        public int CategoryId { get; set; }


        // Properties


        public Category Category { get;set; }

        // Chi tiết hóa đơn
        public virtual IList<OrderDetail> OrderDetails { get; set; }

        public IList<UnitDetail> UnitDetails { get; set; }
    }

    
}
