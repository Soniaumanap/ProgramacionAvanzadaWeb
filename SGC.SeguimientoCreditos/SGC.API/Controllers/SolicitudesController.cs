using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudesServicio _solicitudes;
        private readonly IClientesServicio _clientes;

        public SolicitudesController(
            ISolicitudesServicio solicitudes,
            IClientesServicio clientes)
        {
            _solicitudes = solicitudes;
            _clientes = clientes;
        }

        // POST: api/solicitudes
        // Body: { identificacionCliente, monto, comentarios, usuarioNombre, rol }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearSolicitudRequest req)
        {
            var cliente = await _clientes.ObtenerPorIdentificacionAsync(req.IdentificacionCliente);
            if (cliente == null)
            {
                return BadRequest(new { ok = false, mensaje = "El cliente no está registrado." });
            }

            var resp = await _solicitudes.CrearAsync(
                cliente.Id,
                req.IdentificacionCliente,
                req.Monto,
                req.Comentarios,
                req.UsuarioNombre,
                req.Rol
            );

            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }
    }

    public class CrearSolicitudRequest
    {
        public string IdentificacionCliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string? Comentarios { get; set; }
        public string UsuarioNombre { get; set; } = null!;
        public string Rol { get; set; } = null!; // ServicioCliente o Admin
    }
}
