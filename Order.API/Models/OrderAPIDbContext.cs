using Microsoft.EntityFrameworkCore;
using Order.API.Models.Entities;

namespace Order.API.Models
{
    public class OrderAPIDbContext : DbContext
    {
        public OrderAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order.API.Models.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
