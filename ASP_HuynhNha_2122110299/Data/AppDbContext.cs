using ASP_HuynhNha_2122110299.Model;
using Microsoft.EntityFrameworkCore;

namespace ASP_HuynhNha_2122110299.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập quan hệ giữa Product và Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany() // hoặc .WithMany(c => c.Products) nếu bạn có list sản phẩm trong Category
                .HasForeignKey(p => p.CategoryId)
           
                .OnDelete(DeleteBehavior.Cascade); // Hoặc Restrict nếu muốn cứng hơn

            modelBuilder.Entity<OrderDetail>()
               .HasOne(od => od.Order)
               .WithMany(o => o.OrderDetails)
               .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

        }



    }
}