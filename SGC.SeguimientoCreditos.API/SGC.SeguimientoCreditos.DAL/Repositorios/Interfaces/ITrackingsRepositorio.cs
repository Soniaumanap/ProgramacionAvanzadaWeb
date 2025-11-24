using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces
{
    public interface ITrackingsRepositorio
    {
        Task AgregarAsync(TrackingGestion t);
        Task<List<TrackingGestion>> ObtenerPorGestionAsync(int gestionId);
    }
}
