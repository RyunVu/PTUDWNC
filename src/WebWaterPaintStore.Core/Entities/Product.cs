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

        // Metadata (làm đến lab5 sẽ hiểu)
        public string Meta { get; set; }

        // Tên định danh 
        public string UrlSlug { get; set;}

        // Đơn vị tính
        public string Unit { get; set; }

        // Giá
        public int Price { get; set; }

        // Số lượng tồn
        public int Quantity { get; set; }

        // % giảm giá
        public float Discount { get; set; }

        // Hiển thị trên menu
        public bool Actived { get; set; }

        // Ảnh sản phẩm
        public string ImageUrl { get; set; }

        // Số lượng đã bán
        public int SoldCount { get; set; }

        // Loại của sản phẩm
        public int CategoryId { get; set; }

        // Properties


        public Category Category { get;set; }

        // Chi tiết hóa đơn
        public IList<OrderDetail> OrderDetails { get; set; }
    }
}
