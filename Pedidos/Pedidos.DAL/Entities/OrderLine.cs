using System.ComponentModel.DataAnnotations;

namespace Pedidos.DAL.Entities
{
    public class OrderLine
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public Product Producto { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
