using Pedidos.BO.EntitiesBO;
using System.Collections.Generic;

namespace Pedidos.BL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductBO> GetProducts();
        ProductBO GetProductById(long id);
        long CreateProduct(ProductBO product);
        bool UpdateProduct(long productId, ProductBO product);
        bool DeleteProduct(long productId);
    }
}
