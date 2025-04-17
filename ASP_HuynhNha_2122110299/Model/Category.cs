namespace ASP_HuynhNha_2122110299.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Đảm bảo có danh sách Products
        public ICollection<Product> Products { get; set; }
    }
}