using Pedidos.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DAL.Core
{
    public class ComprasContext : DbContext
    {
        public ComprasContext()
            : base("ComprasConnectionString")
        {

        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<LineaPedido> LineaPedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
