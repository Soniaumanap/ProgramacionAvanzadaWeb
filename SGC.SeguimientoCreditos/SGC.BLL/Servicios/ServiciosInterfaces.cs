using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;

namespace SGC.BLL.Servicios
{
    public interface IUsuariosServicio
    {
        Task<List<UsuarioDto>> ListarAsync();
        Task<CustomResponse<UsuarioDto>> CrearAsync(UsuarioDto dto);
        Task<CustomResponse<UsuarioDto>> ActualizarAsync(UsuarioDto dto);
        Task<bool> EliminarAsync(int id);
        Task<UsuarioDto?> BuscarPorEmailPassAsync(string email, string pass);
        Task<CustomResponse<UsuarioDto>> RegistrarAsync(RegisterDto dto);

    }

    public interface IClientesServicio
    {
        Task<List<ClienteDto>> ListarAsync();
        Task<CustomResponse<ClienteDto>> CrearAsync(ClienteDto dto);
        Task<CustomResponse<ClienteDto>> ActualizarAsync(ClienteDto dto);
        Task<bool> EliminarAsync(int id);
    }

    public interface ISolicitudesServicio
    {
        Task<List<SolicitudDto>> ListarPorRolAsync(Rol rol);
        Task<CustomResponse<SolicitudDto>> CrearAsync(int clienteId, string identificacion, decimal monto, string comentarios, string usuarioNombre, Rol rol);
        Task<CustomResponse<SolicitudDto>> EnviarAprobacionAsync(int gestionId, string usuarioNombre, Rol rol, System.Collections.Generic.List<string>? nuevosDocs = null);
        Task<CustomResponse<SolicitudDto>> AprobarAsync(int gestionId, string usuarioNombre, Rol rol);
        Task<CustomResponse<SolicitudDto>> DevolverAsync(int gestionId, string comentario, string usuarioNombre, Rol rol);
        Task<System.Collections.Generic.List<TrackingDto>> ObtenerTrackingAsync(int gestionId);
    }
}
