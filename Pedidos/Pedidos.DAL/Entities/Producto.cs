using System.ComponentModel.DataAnnotations;

namespace Pedidos.DAL.Entities
{
    public class Producto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public decimal PrecioCompra { get; set; }

        [Required]
        public decimal PrecioAlPorMenor { get; set; }
    }
}
