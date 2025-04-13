using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Models;

namespace OnlineOrderManagementSystem.Data
{
    public class OrderManagementDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public OrderManagementDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Keyboard", Description = "Mechanical", Price = 200, StockQuantity = 10 },
                new Product { Id = 2, Name = "Mouse", Description = "Wireless Mouse", Price = 25, StockQuantity = 100 }
            );
        }
    }
}
