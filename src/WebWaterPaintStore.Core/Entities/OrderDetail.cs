using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public class OrderDetail{

        // Mã đon hàng
        public int OrderId { get; set; }
        // Mã sản phẩm
        public int ProductId { get; set; }
        // Giá sản phẩm
        public int Price { get; set; }
        // Số lượng bán
        public int Quantity { get; set; }
        // % giảm giá
        public float Discount { get; set; }
        // Thành tiền
        public int Total {
            get
            {
                return (int)Math.Round(Price * Quantity * (1 - Discount*0.01), 0);
            }
                
        }

        public virtual Order Order {get; set; }    
        public virtual Product Product { get; set; }
    }
}
