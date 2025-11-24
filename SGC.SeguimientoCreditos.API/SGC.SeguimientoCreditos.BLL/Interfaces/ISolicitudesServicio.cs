using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface ISolicitudesServicio
    {
        Task<List<SolicitudCreditoDto>> ObtenerTodasAsync();
        Task<SolicitudCreditoDto?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(SolicitudCreditoDto dto);
        Task<bool> ActualizarAsync(SolicitudCreditoDto dto);
    }
}
