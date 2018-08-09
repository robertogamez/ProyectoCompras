using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pedidos.DAL.Repository
{
    public interface IRepositoryGeneric<TEntidad>
        where TEntidad : class
    {
        IQueryable<TEntidad> Obtener { get; }

        TEntidad ObtenerPorId(long id);

        void Insertar(TEntidad entidad);

        void Eliminar(TEntidad entidad);

        void Actualizar(TEntidad entidad);

        IQueryable<TEntidad> ObtenerConRelaciones(
            Expression<Func<TEntidad, bool>> condicion,
            params string[] incluir
        );
    }
}
