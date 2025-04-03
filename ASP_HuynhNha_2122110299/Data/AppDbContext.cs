using ASP_HuynhNha_2122110299.Model;
using Microsoft.EntityFrameworkCore;

namespace ASP_HuynhNha_2122110299.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
