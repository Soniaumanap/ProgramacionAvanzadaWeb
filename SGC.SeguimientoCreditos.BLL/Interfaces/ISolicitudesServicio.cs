using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface ISolicitudesServicio
    {
        Task<List<SolicitudDto>> ObtenerTodosAsync();
        Task<SolicitudDto> ObtenerPorIdAsync(int id);
        Task<SolicitudDto> CrearAsync(SolicitudDto dto);
        Task<SolicitudDto> ActualizarAsync(int id, SolicitudDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
