using HuynhNha_2122110299.Model;  // Đảm bảo đúng namespace

namespace HuynhNha_2122110299.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string? Role { get; set; } = "customer"; // admin, staff, customer...
        public string? Avatar { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }



        // ✅ Đơn hàng người dùng đặt
        //public ICollection<Order> Orders { get; set; } = new List<Order>();



    }
}
