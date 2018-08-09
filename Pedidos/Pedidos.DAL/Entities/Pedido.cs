using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.DAL.Entities
{
    public class Pedido
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCliente { get; set; }

        [StringLength(2000)]
        [Required]
        public string Dirección { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}")]
        public string CodigoPostal { get; set; }

        public bool Enviado { get; set; }

        public IEnumerable<LineaPedido> Lineas { get; set; }
    }
}
