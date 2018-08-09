using Pedidos.DAL.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Pedidos.DAL.Repository
{
    public class RepositoryGeneric<TEntidad> : IRepositoryGeneric<TEntidad>
        where TEntidad : class
    {
        #region Variables
        private readonly ShoppingContext _comprasContexto;
        private DbSet<TEntidad> _dbEntidad;
        #endregion

        #region Constructor
        public RepositoryGeneric(ShoppingContext comprasContexto)
        {
            if (comprasContexto == null)
            {
                comprasContexto = new ShoppingContext();
            }

            _comprasContexto = comprasContexto;
            _dbEntidad = comprasContexto.Set<TEntidad>();

        }
        #endregion

        #region Métodos publicos
        public IQueryable<TEntidad> Obtener
        {
            get
            {
                return _dbEntidad;
            }
        }

        public TEntidad ObtenerPorId(long id)
        {
            return _dbEntidad.Find(id);
        }

        public void Actualizar(TEntidad entidad)
        {
            _dbEntidad.Attach(entidad);
            _comprasContexto.Entry(entidad).State = EntityState.Modified;
        }

        public void Eliminar(TEntidad entidad)
        {
            if (_comprasContexto.Entry(entidad).State == EntityState.Detached)
            {
                _dbEntidad.Attach(entidad);
            }

            _dbEntidad.Remove(entidad);
        }

        public void Insertar(TEntidad entidad)
        {
            _dbEntidad.Add(entidad);
        }

        public IQueryable<TEntidad> ObtenerConRelaciones(
            Expression<Func<TEntidad, bool>> condicion,
            params string[] incluir
        )
        {
            IQueryable<TEntidad> consulta =
                this._dbEntidad;

            consulta = incluir.Aggregate(consulta, (current, inc) => current.Include(inc));

            return consulta.Where(condicion);
        }
        #endregion


    }
}
