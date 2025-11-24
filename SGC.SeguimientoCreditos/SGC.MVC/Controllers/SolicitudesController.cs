using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.MVC.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly ISolicitudesServicio _solicitudesServicio;
        private readonly IClientesServicio _clientesServicio;

        public SolicitudesController(
            ISolicitudesServicio solicitudesServicio,
            IClientesServicio clientesServicio)
        {
            _solicitudesServicio = solicitudesServicio;
            _clientesServicio = clientesServicio;
        }

        private bool UsuarioServicio()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            return rol == "ServicioCliente" || rol == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            if (!UsuarioServicio())
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // Clase interna SOLO para esta acción (no es un Model global)
        public class CrearSolicitudRequest
        {
            public string IdentificacionCliente { get; set; } = string.Empty;
            public string Monto { get; set; } = string.Empty;
            public string? Comentarios { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] CrearSolicitudRequest req)
        {
            try
            {
                var usuario = HttpContext.Session.GetString("UsuarioNombre");
                var rol = HttpContext.Session.GetString("UsuarioRol");

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(rol))
                {
                    return Ok(new { ok = false, mensaje = "Debe iniciar sesión." });
                }

                if (!UsuarioServicio())
                {
                    return Ok(new { ok = false, mensaje = "No tiene permisos para crear solicitudes." });
                }

                // Validar datos mínimos
                if (string.IsNullOrWhiteSpace(req.IdentificacionCliente) ||
                    string.IsNullOrWhiteSpace(req.Monto))
                {
                    return Ok(new { ok = false, mensaje = "Los datos de la solicitud son obligatorios." });
                }

                // Convertir monto manualmente
                if (!decimal.TryParse(req.Monto, out var montoDecimal))
                {
                    return Ok(new { ok = false, mensaje = "El monto ingresado no es válido." });
                }

                var cliente = await _clientesServicio.ObtenerPorIdentificacionAsync(req.IdentificacionCliente);
                if (cliente == null)
                {
                    return Ok(new { ok = false, mensaje = "El cliente no está registrado." });
                }

                var resp = await _solicitudesServicio.CrearAsync(
                    cliente.Id,
                    req.IdentificacionCliente,
                    montoDecimal,
                    req.Comentarios ?? string.Empty,
                    usuario,
                    rol
                );

                // AQUÍ: si resp.Ok == true → Éxito, si false → error de negocio
                return Ok(new
                {
                    ok = resp.Ok,
                    mensaje = resp.Mensaje
                });
            }
            catch (Exception)
            {
                // Si pasa cualquier cosa rara, devolvemos ok=false pero SIEMPRE 200
                return Ok(new
                {
                    ok = false,
                    mensaje = "Ocurrió un error interno al crear la solicitud."
                });
            }
        }
    }
}
