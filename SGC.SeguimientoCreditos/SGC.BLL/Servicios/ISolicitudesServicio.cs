using SGC.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC.BLL.Servicios
{
    public interface ISolicitudesServicio
    {
        Task<List<SolicitudDto>> ListarPorRolAsync(string rol);
        Task<CustomResponse<SolicitudDto>> CrearAsync(
            int clienteId,
            string identificacionCliente,
            decimal monto,
            string? comentarios,
            string usuarioNombre,
            string rol);

        Task<CustomResponse<SolicitudDto>> EnviarAprobacionAsync(
            int gestionId,
            string usuarioNombre,
            string rol);

        Task<CustomResponse<SolicitudDto>> AprobarAsync(
            int gestionId,
            string usuarioNombre,
            string rol);

        Task<CustomResponse<SolicitudDto>> DevolverAsync(
            int gestionId,
            string comentario,
            string usuarioNombre,
            string rol);

        Task<List<TrackingDto>> ObtenerTrackingAsync(int gestionId);

        Task<CustomResponse<bool>> EliminarAsync(int gestionId, string usuarioNombre, string rol);

    }
}
