using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface IClientesServicio
    {
        Task<List<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto> ObtenerPorIdAsync(int id);
        Task<ClienteDto> CrearAsync(ClienteDto dto);
        Task<ClienteDto> ActualizarAsync(int id, ClienteDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
