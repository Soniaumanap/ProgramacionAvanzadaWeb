using SGC.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC.BLL.Servicios
{
    public interface IUsuariosServicio
    {
        Task<List<UsuarioDto>> ListarAsync();
        Task<UsuarioDto?> ObtenerAsync(int id);
        Task<CustomResponse<UsuarioDto>> CrearAsync(UsuarioDto dto);
        Task<CustomResponse<UsuarioDto>> ActualizarAsync(UsuarioDto dto);
        Task<CustomResponse<bool>> EliminarAsync(int id);
        Task<UsuarioDto?> LoginAsync(string email, string password);
        Task<CustomResponse<UsuarioDto>> RegistrarAsync(UsuarioDto dto);
    }
}
