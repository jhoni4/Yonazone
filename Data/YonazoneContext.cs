using Microsoft.EntityFrameworkCore;
using Yonazone.Models;

namespace Yonazone.Data
{
    public class YonazoneContext : DbContext
    {
        public YonazoneContext(DbContextOptions<YonazoneContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<ProductSubType> ProductSubType {get; set;}
    }
}