using System.ComponentModel.DataAnnotations;

namespace Pedidos.BO.EntitiesBO
{
    public class LineaPedidoBO
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public ProductoBO Producto { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
