using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces
{
    public interface ISolicitudesRepositorio
    {
        Task<bool> AgregarAsync(SolicitudCredito s);
        Task<bool> ActualizarAsync(SolicitudCredito s);
        Task<SolicitudCredito?> ObtenerPorIdAsync(int id);
        Task<List<SolicitudCredito>> ObtenerTodasAsync();
    }
}
