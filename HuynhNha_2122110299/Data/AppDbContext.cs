using HuynhNha_2122110299.Model;
using Microsoft.EntityFrameworkCore;

namespace HuynhNha_2122110299.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        // DbSet các bảng
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<TinTuc> TinTuc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ======================= PRODUCT ========================

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
    .HasKey(p => p.ProductId); // Đặt ProductId làm khóa chính

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd(); // Tự động tăng khóa chính
            modelBuilder.Entity<Product>()
    .Property(p => p.ImageUrl)
    .IsRequired() // Không cho phép NULL
    .HasMaxLength(500) // Độ dài tối đa của đường dẫn
    .HasColumnType("nvarchar(500)");
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Brand)
            //    .WithMany(b => b.Products)
            //    .HasForeignKey(p => p.BrandId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // ======================= BRAND ========================


            // ======================= CATEGORY ========================

            modelBuilder.Entity<Category>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .HasConstraintName("FK_Categories_Users_CreatedBy")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedBy)
                .HasConstraintName("FK_Categories_Users_UpdatedBy")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.DeletedByUser)
                .WithMany()
                .HasForeignKey(c => c.DeletedBy)
                .HasConstraintName("FK_Categories_Users_DeletedBy")
                .OnDelete(DeleteBehavior.Restrict);

            // ======================= ORDER ========================

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .HasConstraintName("FK_Orders_Users_UserId")
                .OnDelete(DeleteBehavior.Cascade);

         
            // ======================= ORDER DETAIL ========================

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.CreatedByUser)
                .WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_OrderDetails_Users_CreatedBy")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.UpdatedByUser)
                .WithMany()
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_OrderDetails_Users_UpdatedBy")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.DeletedByUser)
                .WithMany()
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_OrderDetails_Users_DeletedBy")
                .OnDelete(DeleteBehavior.Restrict);

            // ======================= DECIMAL CONFIG ========================

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.Total).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(d => d.Price).HasPrecision(18, 2);
        }
    }
}

