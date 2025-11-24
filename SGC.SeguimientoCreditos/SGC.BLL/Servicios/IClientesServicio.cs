using SGC.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC.BLL.Servicios
{
    public interface IClientesServicio
    {
        Task<CustomResponse<ClienteDto>> CrearAsync(ClienteDto dto);
        Task<CustomResponse<bool>> ActualizarAsync(ClienteDto dto);
        Task<CustomResponse<bool>> EliminarAsync(int id);

        Task<List<ClienteDto>> ListarAsync();
        Task<ClienteDto?> ObtenerAsync(int id);
        Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion);
    }
}
