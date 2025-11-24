using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface ITrackingsServicio
    {
        Task<List<TrackingGestionDto>> ObtenerPorGestionAsync(int gestionId);
        Task CrearAsync(TrackingGestionDto dto);
    }
}
