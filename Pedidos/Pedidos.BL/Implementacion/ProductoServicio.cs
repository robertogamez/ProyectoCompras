using AutoMapper;
using Pedidos.BL.Interfaces;
using Pedidos.BO.EntitiesBO;
using Pedidos.DAL.Entities;
using Pedidos.DAL.UnidadDeTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pedidos.BL.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        #region Variables
        private readonly UnidadDeTrabajo _unidadDeTrabajo;
        #endregion

        #region Constructor
        public ProductoServicio()
        {

        }

        public ProductoServicio(UnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        #endregion

        public IEnumerable<ProductoBO> ObtenerTodosProductos()
        {
            var productos = _unidadDeTrabajo
                .ProductosRepositorio
                .Obtener.ToList();

            var modeloProductos
                = Mapper.Map<List<Producto>, List<ProductoBO>>(productos);

            return modeloProductos;
        }

        public ProductoBO ObtenerProductoPorId(long id)
        {
            var producto = _unidadDeTrabajo.ProductosRepositorio.ObtenerPorId(id);

            if(producto != null)
            {
                var productoBO = Mapper.Map<Producto, ProductoBO>(producto);
                return productoBO;
            }

            return null;
        }

        public long CrearProducto(ProductoBO producto)
        {
            using (var scope = new TransactionScope())
            {
                var nuevoProducto = Mapper.Map<ProductoBO, Producto>(producto);

                _unidadDeTrabajo.ProductosRepositorio.Insertar(nuevoProducto);
                _unidadDeTrabajo.Save();
                scope.Complete();

                return nuevoProducto.Id;
            } 
        }

        public bool ActualizarProducto(long productoId, ProductoBO producto)
        {
            var exitoso = false;

            if(producto != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productoEncontrado = _unidadDeTrabajo.ProductosRepositorio.ObtenerPorId(productoId);

                    if(productoEncontrado != null)
                    {
                        productoEncontrado.Name = producto.Name;
                        productoEncontrado.PrecioAlPorMenor = producto.PrecioAlPorMenor;
                        productoEncontrado.PrecioCompra = producto.PrecioCompra;

                        _unidadDeTrabajo.ProductosRepositorio.Actualizar(productoEncontrado);
                        _unidadDeTrabajo.Save();
                        scope.Complete();
                        exitoso = true;
                    }
                }
            }

            return exitoso;
        }
    }
}
