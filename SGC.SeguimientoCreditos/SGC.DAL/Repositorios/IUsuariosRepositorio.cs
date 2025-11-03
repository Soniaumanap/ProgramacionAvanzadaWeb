using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<List<Usuario>> ObtenerUsuariosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> AgregarAsync(Usuario u);
        Task<bool> ActualizarAsync(Usuario u);
        Task<bool> EliminarAsync(int id);
    }
}
