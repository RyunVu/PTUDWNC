using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public enum OrderStatus
    {
        New,        // Đơn hàng mới
        Cancelled,  // Đã hủy
        Approved,   // Đã xác nhận
        Shipping,   // Đang giao hàng
        Returned,   // Trả hàng
        Success     // Đã giao hàng thành công
    }

    public class Order : IEntity{
        // Mã đơn hàng
        public int Id { get; set; }

        // Ngày đặt hàng
        public DateTime? OrderDate { get; set; }   

        // Tên người nhận hàng
        public string ShipName { get; set; }

        // Địa chỉ giao hàng
        public string ShipAddress { get; set; }

        // Email người nhận
        public string Email { get; set; }

        // Số điện thoại của người nhận hàng
        public string ShipTel { get; set; }

        // Trạng thái đơn hàng
        public OrderStatus Status { get; set; }

        // Ghi chú thêm 
        public string Notes { get; set; }

           
        public int UserId { get; set; }

        public User User { get; set; }
        // Properties

        // Chi tiết hóa đơn
        public IList<OrderDetail> OrderDetails { get; set; }


    }
}
