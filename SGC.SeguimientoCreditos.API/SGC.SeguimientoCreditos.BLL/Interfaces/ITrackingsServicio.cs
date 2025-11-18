using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface ITrackingsServicio
    {
        Task<List<TrackingDto>> ObtenerPorSolicitudAsync(int solicitudId);
        Task<TrackingDto> CrearAsync(TrackingDto dto);
    }
}
