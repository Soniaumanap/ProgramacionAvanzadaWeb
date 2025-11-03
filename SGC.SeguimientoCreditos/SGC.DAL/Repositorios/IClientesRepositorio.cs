using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public interface IClientesRepositorio
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> AgregarAsync(Cliente c);
        Task<bool> ActualizarAsync(Cliente c);
        Task<bool> EliminarAsync(int id);
    }
}
