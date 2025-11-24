using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<Usuario?> ObtenerPorIdentificacionAsync(string id);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<bool> AgregarAsync(Usuario u);
        Task<bool> ActualizarAsync(Usuario u);
        Task<bool> EliminarAsync(int id);
        Task<List<Usuario>> ListarAsync();
        Task<Usuario?> ObtenerAsync(int id);
    }
}
