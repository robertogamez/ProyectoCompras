using System.ComponentModel.DataAnnotations;

namespace Pedidos.BO.EntitiesBO
{
    public class OrderLineBO
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public ProductBO Producto { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
