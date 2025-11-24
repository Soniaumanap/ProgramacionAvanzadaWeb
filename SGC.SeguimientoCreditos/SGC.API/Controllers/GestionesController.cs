using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionesController : ControllerBase
    {
        private readonly ISolicitudesServicio _solicitudes;

        public GestionesController(ISolicitudesServicio solicitudes)
        {
            _solicitudes = solicitudes;
        }

        // GET: api/gestiones?rol=Analista
        [HttpGet]
        public async Task<IActionResult> ListarPorRol([FromQuery] string rol)
        {
            var lista = await _solicitudes.ListarPorRolAsync(rol);
            return Ok(lista);
        }

        // POST: api/gestiones/{id}/enviar-aprobacion
        [HttpPost("{id:int}/enviar-aprobacion")]
        public async Task<IActionResult> EnviarAprobacion(
            int id,
            [FromBody] AccionGestionRequest req)
        {
            var resp = await _solicitudes.EnviarAprobacionAsync(id, req.UsuarioNombre, req.Rol);

            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // POST: api/gestiones/{id}/aprobar
        [HttpPost("{id:int}/aprobar")]
        public async Task<IActionResult> Aprobar(
            int id,
            [FromBody] AccionGestionRequest req)
        {
            var resp = await _solicitudes.AprobarAsync(id, req.UsuarioNombre, req.Rol);

            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // POST: api/gestiones/{id}/devolver
        [HttpPost("{id:int}/devolver")]
        public async Task<IActionResult> Devolver(
            int id,
            [FromBody] DevolverGestionRequest req)
        {
            var resp = await _solicitudes.DevolverAsync(id, req.Comentario, req.UsuarioNombre, req.Rol);

            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // GET: api/gestiones/{id}/tracking
        [HttpGet("{id:int}/tracking")]
        public async Task<IActionResult> Tracking(int id)
        {
            var lista = await _solicitudes.ObtenerTrackingAsync(id);
            return Ok(lista);
        }
    }

    public class AccionGestionRequest
    {
        public string UsuarioNombre { get; set; } = null!;
        public string Rol { get; set; } = null!; // Analista, Gestor, Admin
    }

    public class DevolverGestionRequest : AccionGestionRequest
    {
        public string Comentario { get; set; } = null!;
    }
}
