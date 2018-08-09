using Pedidos.BO.EntitiesBO;
using System.Collections.Generic;

namespace Pedidos.BL.Interfaces
{
    public interface IProductoServicio
    {
        IEnumerable<ProductoBO> ObtenerTodosProductos();
        ProductoBO ObtenerProductoPorId(long id);
        long CrearProducto(ProductoBO producto);
        bool ActualizarProducto(long productoId, ProductoBO producto);
        //bool EliminarProducto(ProductoBO producto);
    }
}
