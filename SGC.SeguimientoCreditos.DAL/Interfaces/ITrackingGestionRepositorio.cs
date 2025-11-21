using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Interfaces
{
    public interface ITrackingGestionRepositorio : IRepositorioGenerico<TrackingGestion>
    {
        Task<IEnumerable<TrackingGestion>> ObtenerPorSolicitud(int solicitudCreditoId);
    }
}
