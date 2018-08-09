using Pedidos.DAL.Core;
using Pedidos.DAL.Entities;
using Pedidos.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace Pedidos.DAL.UnitWork
{
    public class UnitOfWork
    {
        #region Variables
        private ShoppingContext _shoppingContext;
        private IRepositoryGeneric<Order> _ordersRepository;
        private IRepositoryGeneric<Product> _productsRepository;
        private IRepositoryGeneric<OrderLine> _orderLinesRepository;
        #endregion

        #region Constructor
        public UnitOfWork(
            ShoppingContext shoppingContext,
            IRepositoryGeneric<Order> ordersRepository,
            IRepositoryGeneric<Product> productsRepository,
            IRepositoryGeneric<OrderLine> orderLinesRepository
        )
        {
            if (shoppingContext == null)
            {
                _shoppingContext = new ShoppingContext();
            }

            _shoppingContext = shoppingContext;
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _orderLinesRepository = orderLinesRepository;
        }
        #endregion

        #region Métodos publicos
        public IRepositoryGeneric<Order> PedidosRepositorio
        {
            get
            {
                if (_ordersRepository == null)
                {
                    _ordersRepository = new RepositoryGeneric<Order>(_shoppingContext);
                }

                return _ordersRepository;
            }
        }

        public IRepositoryGeneric<Product> ProductosRepositorio
        {
            get
            {
                if (_productsRepository == null)
                {
                    _productsRepository = new RepositoryGeneric<Product>(_shoppingContext);
                }

                return _productsRepository;
            }
        }

        public IRepositoryGeneric<OrderLine> LineasPedidoRepositorio
        {
            get
            {
                if (_orderLinesRepository == null)
                {
                    _orderLinesRepository = new RepositoryGeneric<OrderLine>(_shoppingContext);
                }

                return _orderLinesRepository;
            }
        }

        public void Save()
        {
            try
            {
                _shoppingContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // TODO: Logging
                var errorMessages = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    errorMessages.Add(string.Format("{0}: Tipo de la Entidad \"{1}\" su estado \"{2}\" tiene los siguientes errores de validación:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorMessages.Add(string.Format("- Propiedad: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                throw new Exception(errorMessages.ToString());
            }
        }
        #endregion
    }
}
