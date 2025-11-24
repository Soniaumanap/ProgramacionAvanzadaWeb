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

        [HttpPost]
        public async Task<IActionResult> Crear(
            string identificacionCliente,
            decimal monto,
            string comentarios)
        {
            if (!UsuarioServicio())
                return Unauthorized();

            var usuario = HttpContext.Session.GetString("UsuarioNombre");
            var rol = HttpContext.Session.GetString("UsuarioRol");

            var cliente = await _clientesServicio.ObtenerPorIdentificacionAsync(identificacionCliente);
            if (cliente == null)
            {
                return Json(new { ok = false, mensaje = "El cliente no está registrado." });
            }

            var resp = await _solicitudesServicio.CrearAsync(
                cliente.Id,
                identificacionCliente,
                monto,
                comentarios,
                usuario!,
                rol!
            );

            return Json(resp);
        }
    }
}
