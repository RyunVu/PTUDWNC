using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public class Category : IEntity{
        // Mã loại
        public int Id { get; set; }

        // Tên sản phẩm
        public string Name { get; set; }

        // Tên định danh
        public string UrlSlug { get; set; }

        // Mô tả về loại
        public string Description { get; set; }

        // Hiển thị trên menu
        public bool Actived { get; set; }

        // Danh sách các sản phẩm thuộc loại này
        public IList<Product> Products { get; set;}

    }
}
