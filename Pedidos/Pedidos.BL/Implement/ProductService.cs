using AutoMapper;
using Pedidos.BL.Interfaces;
using Pedidos.BO.EntitiesBO;
using Pedidos.DAL.Entities;
using Pedidos.DAL.UnitWork;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Pedidos.BL.Implement
{
    public class ProductService : IProductService
    {
        #region Variables
        private readonly UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ProductService()
        {

        }

        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<ProductBO> GetProducts()
        {
            // Obtener todos los productos activos
            var products = _unitOfWork
                .ProductosRepositorio
                .Obtener
                .Where(p => p.Status)
                .ToList();

            var modeloProductos
                = Mapper.Map<List<Product>, List<ProductBO>>(products);

            return modeloProductos;
        }

        public ProductBO GetProductById(long id)
        {
            var producto = _unitOfWork.ProductosRepositorio.ObtenerPorId(id);

            if (producto != null)
            {
                var productoBO = Mapper.Map<Product, ProductBO>(producto);
                return productoBO;
            }

            return null;
        }

        public long CreateProduct(ProductBO product)
        {
            using (var scope = new TransactionScope())
            {
                product.Status = true;
                var newProduct = Mapper.Map<ProductBO, Product>(product);

                _unitOfWork.ProductosRepositorio.Insertar(newProduct);
                _unitOfWork.Save();
                scope.Complete();

                return newProduct.Id;
            }
        }

        public bool UpdateProduct(long productoId, ProductBO product)
        {
            var success = false;

            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productoEncontrado = _unitOfWork.ProductosRepositorio.ObtenerPorId(productoId);

                    if (productoEncontrado != null)
                    {
                        productoEncontrado.Name = product.Name;
                        productoEncontrado.PrecioAlPorMenor = product.PrecioAlPorMenor;
                        productoEncontrado.PrecioCompra = product.PrecioCompra;

                        _unitOfWork.ProductosRepositorio.Actualizar(productoEncontrado);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }

            return success;
        }

        public bool DeleteProduct(long productoId)
        {
            var exitoso = false;

            using (var scope = new TransactionScope())
            {
                var productoEncontrado = _unitOfWork.ProductosRepositorio.ObtenerPorId(productoId);

                if (productoEncontrado != null)
                {
                    // Borrado lógico
                    productoEncontrado.Status = false;

                    _unitOfWork.ProductosRepositorio.Actualizar(productoEncontrado);
                    _unitOfWork.Save();
                    scope.Complete();
                    exitoso = true;
                }
            }

            return exitoso;
        }
    }
}
