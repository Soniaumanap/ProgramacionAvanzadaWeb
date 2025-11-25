using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionesController : ControllerBase
    {
        private readonly ISolicitudesServicio _solicitudesServicio;

        public GestionesController(ISolicitudesServicio solicitudesServicio)
        {
            _solicitudesServicio = solicitudesServicio;
        }

        // ======== MODELOS SOLO PARA EL API (REQUESTS) =========

        public class AccionGestionRequest
        {
            public string UsuarioNombre { get; set; } = string.Empty;
            public string Rol { get; set; } = string.Empty; // "Analista", "Gestor", "Admin", etc.
        }

        public class DevolverGestionRequest : AccionGestionRequest
        {
            public string Comentario { get; set; } = string.Empty;
        }

        // ================== ENDPOINTS ==================

        /// <summary>
        /// Lista gestiones según el rol indicado
        /// GET /api/gestiones?rol=Analista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarPorRol([FromQuery] string rol)
        {
            var lista = await _solicitudesServicio.ListarPorRolAsync(rol);
            return Ok(lista); // devuelve la lista de SolicitudDto
        }

        /// <summary>
        /// Enviar una gestión a aprobación
        /// POST /api/gestiones/{id}/enviar-aprobacion
        /// </summary>
        [HttpPost("{id:int}/enviar-aprobacion")]
        public async Task<IActionResult> EnviarAprobacion(
            int id,
            [FromBody] AccionGestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UsuarioNombre) ||
                string.IsNullOrWhiteSpace(request.Rol))
            {
                return BadRequest(new { ok = false, mensaje = "UsuarioNombre y Rol son requeridos." });
            }

            var resp = await _solicitudesServicio.EnviarAprobacionAsync(id, request.UsuarioNombre, request.Rol);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        /// <summary>
        /// Aprobar una gestión
        /// POST /api/gestiones/{id}/aprobar
        /// </summary>
        [HttpPost("{id:int}/aprobar")]
        public async Task<IActionResult> Aprobar(
            int id,
            [FromBody] AccionGestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UsuarioNombre) ||
                string.IsNullOrWhiteSpace(request.Rol))
            {
                return BadRequest(new { ok = false, mensaje = "UsuarioNombre y Rol son requeridos." });
            }

            var resp = await _solicitudesServicio.AprobarAsync(id, request.UsuarioNombre, request.Rol);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        /// <summary>
        /// Devolver una gestión a reproceso
        /// POST /api/gestiones/{id}/devolver
        /// </summary>
        [HttpPost("{id:int}/devolver")]
        public async Task<IActionResult> Devolver(
            int id,
            [FromBody] DevolverGestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UsuarioNombre) ||
                string.IsNullOrWhiteSpace(request.Rol) ||
                string.IsNullOrWhiteSpace(request.Comentario))
            {
                return BadRequest(new { ok = false, mensaje = "UsuarioNombre, Rol y Comentario son requeridos." });
            }

            var resp = await _solicitudesServicio.DevolverAsync(id, request.Comentario, request.UsuarioNombre, request.Rol);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        /// <summary>
        /// Eliminar una gestión (no aprobada)
        /// POST /api/gestiones/{id}/eliminar
        /// </summary>
        [HttpPost("{id:int}/eliminar")]
        public async Task<IActionResult> Eliminar(
            int id,
            [FromBody] AccionGestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UsuarioNombre) ||
                string.IsNullOrWhiteSpace(request.Rol))
            {
                return BadRequest(new { ok = false, mensaje = "UsuarioNombre y Rol son requeridos." });
            }

            var resp = await _solicitudesServicio.EliminarAsync(id, request.UsuarioNombre, request.Rol);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        /// <summary>
        /// Obtener el tracking de una gestión
        /// GET /api/gestiones/{id}/tracking
        /// </summary>
        [HttpGet("{id:int}/tracking")]
        public async Task<IActionResult> Tracking(int id)
        {
            var lista = await _solicitudesServicio.ObtenerTrackingAsync(id);
            return Ok(lista); // lista de TrackingDto
        }
    }
}
