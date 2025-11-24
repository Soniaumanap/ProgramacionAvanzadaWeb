using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios.Interfaces
{
    public interface ITrackingsRepositorio
    {
        Task<bool> AgregarAsync(TrackingGestion t);
        Task<List<TrackingGestion>> ListarPorGestionAsync(int gestionId);
    }
}
