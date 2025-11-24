using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<bool> AgregarAsync(Usuario u);
        Task<bool> ActualizarAsync(Usuario u);
        Task<bool> EliminarAsync(int id);
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<List<Usuario>> ObtenerUsuariosAsync();
    }
}
