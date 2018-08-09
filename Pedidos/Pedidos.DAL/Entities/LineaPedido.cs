using System.ComponentModel.DataAnnotations;

namespace Pedidos.DAL.Entities
{
    public class LineaPedido
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
