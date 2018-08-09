using Pedidos.DAL.Entities;
using System.Data.Entity;

namespace Pedidos.DAL.Core
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext()
            : base("ComprasConnectionString")
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
