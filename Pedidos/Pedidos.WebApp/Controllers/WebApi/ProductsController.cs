using Pedidos.BL.Interfaces;
using Pedidos.BO.EntitiesBO;
using Pedidos.WebApp.VistaModelos;
using System;
using System.Linq;
using System.Web.Http;

namespace Pedidos.WebApp.Controllers.WebApi
{
    public class ProductsController : ApiController
    {
        public IProductService ProductService { get; set; }

        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        public IHttpActionResult Get([FromUri]DataTableAjaxRequestModel model)
        {
            try
            {
                var products = ProductService.GetProducts();

                var resultado = new
                {
                    draw = model.draw,
                    recordsTotal = products.Count(),
                    recordsFiltered = products.Count(),
                    data = products,
                    error = ""
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetProducto(long id)
        {
            try
            {
                var producto = ProductService.GetProductById(id);

                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCrearProducto(ProductBO producto)
        {
            try
            {
                long nuevoProductoId = ProductService.CreateProduct(producto);

                if (nuevoProductoId > 0)
                {
                    return Ok(nuevoProductoId);
                }
                else
                {
                    return BadRequest("No se pudo crear el producto");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutActualizarProducto(long id, ProductBO producto)
        {
            try
            {
                bool productoActualizado = ProductService.UpdateProduct(id, producto);

                if (productoActualizado)
                {
                    return Ok(productoActualizado);
                }
                else
                {
                    return BadRequest("No se pudo actualizar el Producto");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult DeleteActualizarProducto(long id)
        {
            try
            {
                bool productoEliminado = ProductService.DeleteProduct(id);

                if (productoEliminado)
                {
                    return Ok(productoEliminado);
                }
                else
                {
                    return BadRequest("No se pudo actualizar el Producto");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
