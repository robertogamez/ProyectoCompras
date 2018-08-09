using System.ComponentModel.DataAnnotations;

namespace Pedidos.BO.EntitiesBO
{
    public class ProductBO
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public decimal PrecioCompra { get; set; }

        [Required]
        public decimal PrecioAlPorMenor { get; set; }

        public bool Status { get; set; }
    }
}
