using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface IClientesServicio
    {
        Task<List<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto?> ObtenerPorIdAsync(int id);
        Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> CrearAsync(ClienteDto dto);
        Task<bool> ActualizarAsync(ClienteDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
