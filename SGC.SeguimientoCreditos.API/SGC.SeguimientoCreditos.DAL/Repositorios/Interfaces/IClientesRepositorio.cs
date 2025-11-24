using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces
{
    public interface IClientesRepositorio
    {
        Task<bool> AgregarAsync(Cliente c);
        Task<bool> ActualizarAsync(Cliente c);
        Task<bool> EliminarAsync(int id);
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
    }
}
