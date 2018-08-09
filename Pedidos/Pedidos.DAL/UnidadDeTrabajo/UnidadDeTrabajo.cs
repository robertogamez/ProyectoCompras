using Pedidos.DAL.Core;
using Pedidos.DAL.Entities;
using Pedidos.DAL.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace Pedidos.DAL.UnidadDeTrabajo
{
    public class UnidadDeTrabajo
    {
        #region Variables
        private ComprasContext _comprasContexto;
        private IRepositorioGenerico<Pedido> _pedidosRepositorio;
        private IRepositorioGenerico<Producto> _productosRepositorio;
        private IRepositorioGenerico<LineaPedido> _lineasPedidoRepositorio;
        #endregion

        #region Constructor
        public UnidadDeTrabajo(
            ComprasContext comprasContexto,
            IRepositorioGenerico<Pedido> pedidosRepositorio,
            IRepositorioGenerico<Producto> productosRepositorio,
            IRepositorioGenerico<LineaPedido> lineasPedidoRepositorio
        )
        {
            if (comprasContexto == null)
            {
                _comprasContexto = new ComprasContext();
            }

            _comprasContexto = comprasContexto;
            _pedidosRepositorio = pedidosRepositorio;
            _productosRepositorio = productosRepositorio;
            _lineasPedidoRepositorio = lineasPedidoRepositorio;
        }
        #endregion

        #region Métodos publicos
        public IRepositorioGenerico<Pedido> PedidosRepositorio
        {
            get
            {
                if (_pedidosRepositorio == null)
                {
                    _pedidosRepositorio = new RepositorioGenerico<Pedido>(_comprasContexto);
                }

                return _pedidosRepositorio;
            }
        }

        public IRepositorioGenerico<Producto> ProductosRepositorio
        {
            get
            {
                if (_productosRepositorio == null)
                {
                    _productosRepositorio = new RepositorioGenerico<Producto>(_comprasContexto);
                }

                return _productosRepositorio;
            }
        }

        public IRepositorioGenerico<LineaPedido> LineasPedidoRepositorio
        {
            get
            {
                if (_lineasPedidoRepositorio == null)
                {
                    _lineasPedidoRepositorio = new RepositorioGenerico<LineaPedido>(_comprasContexto);
                }

                return _lineasPedidoRepositorio;
            }
        }

        public void Save()
        {
            try
            {
                _comprasContexto.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // TODO: Logging
                var mensajesError = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    mensajesError.Add(string.Format("{0}: Tipo de la Entidad \"{1}\" su estado \"{2}\" tiene los siguientes errores de validación:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        mensajesError.Add(string.Format("- Propiedad: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                throw new Exception(mensajesError.ToString());
            }
        }
        #endregion
    }
}
