using System.Linq.Expressions;

namespace SGC.SeguimientoCreditos.DAL.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicado);

        Task<T> AgregarAsync(T entidad);
        Task<T> ActualizarAsync(T entidad);
        Task<bool> EliminarAsync(int id);
    }
}
