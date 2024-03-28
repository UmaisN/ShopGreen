using Microsoft.EntityFrameworkCore;
using ShopGreen.Server.Models;

namespace ShopGreen.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public AppDbContext() { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<Purchases> Purchases { get; set; }

    }
}
