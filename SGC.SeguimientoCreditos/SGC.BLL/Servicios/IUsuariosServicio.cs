using SGC.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC.BLL.Servicios
{
    public interface IUsuariosServicio
    {
        Task<CustomResponse<UsuarioDto>> RegistrarAsync(UsuarioDto dto);
        Task<UsuarioDto?> LoginAsync(string email, string password);

        Task<List<UsuarioDto>> ListarAsync();
        Task<UsuarioDto?> ObtenerAsync(int id);
        Task<CustomResponse<bool>> ActualizarAsync(UsuarioDto dto);
        Task<CustomResponse<bool>> EliminarAsync(int id);
    }
}
