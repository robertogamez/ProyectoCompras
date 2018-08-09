using Pedidos.BL.Interfaces;
using Pedidos.BO.EntitiesBO;
using Pedidos.WebApp.VistaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pedidos.WebApp.Controllers.WebApi
{
    public class ProductosController : ApiController
    {
        public IProductoServicio ProductoServicios { get; set; }

        public ProductosController(IProductoServicio productoServicio)
        {
            ProductoServicios = productoServicio;
        }

        public IHttpActionResult Get([FromUri]DataTableAjaxRequestModel model)
        {
            try
            {
                var productos = ProductoServicios.ObtenerTodosProductos();

                var resultado = new
                {
                    draw = model.draw,
                    recordsTotal = productos.Count(),
                    recordsFiltered = productos.Count(),
                    data = productos,
                    error = ""
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCrearProducto(ProductoBO producto)
        {
            try
            {
                long nuevoProductoId = ProductoServicios.CrearProducto(producto);

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

        public IHttpActionResult PutActualizarProducto(long productoId, ProductoBO producto)
        {
            try
            {
                bool productoActualizado = ProductoServicios.ActualizarProducto(productoId, producto);

                if (productoActualizado)
                {
                    return Ok();
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
