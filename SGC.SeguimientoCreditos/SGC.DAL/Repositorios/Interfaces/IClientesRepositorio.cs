using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios.Interfaces
{
    public interface IClientesRepositorio
    {
        Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> AgregarAsync(Cliente c);
        Task<bool> ActualizarAsync(Cliente c);
        Task<bool> EliminarAsync(int id);
        Task<List<Cliente>> ListarAsync();
        Task<Cliente?> ObtenerAsync(int id);
    }
}
