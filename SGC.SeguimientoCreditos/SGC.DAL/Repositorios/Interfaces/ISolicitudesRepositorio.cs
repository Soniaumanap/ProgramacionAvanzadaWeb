using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios.Interfaces
{
    public interface ISolicitudesRepositorio
    {
        Task<bool> AgregarAsync(SolicitudCredito s);
        Task<bool> ActualizarAsync(SolicitudCredito s);
        Task<SolicitudCredito?> ObtenerAsync(int id);
        Task<List<SolicitudCredito>> ListarAsync();
        Task<SolicitudCredito?> ObtenerPorClienteActivo(string identificacion);
    }
}
