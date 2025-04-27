

namespace HuynhNha_2122110299.Model
{
    public class Order
    {
        public int OrderId { get; set; }

        // Người đặt hàng
        public int UserId { get; set; }
        public User? User { get; set; }

        // Thông tin người nhận
   
        // Tổng tiền, phương thức thanh toán
        public decimal Total{ get; set; }
      
        public int? OrderDate { get; set; }
  
        // Quan hệ với chi tiết đơn hàng
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
