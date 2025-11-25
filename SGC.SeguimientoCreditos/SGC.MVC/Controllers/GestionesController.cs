using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.MVC.Controllers
{
    public class GestionesController : Controller
    {
        private readonly ISolicitudesServicio _solicitudesServicio;

        public GestionesController(ISolicitudesServicio solicitudesServicio)
        {
            _solicitudesServicio = solicitudesServicio;
        }

        private bool EstaLogueado() =>
            !string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"));

        public async Task<IActionResult> Index()
        {
            if (!EstaLogueado())
                return RedirectToAction("Login", "Auth");

            var rol = HttpContext.Session.GetString("UsuarioRol") ?? string.Empty;

            var lista = await _solicitudesServicio.ListarPorRolAsync(rol);

            ViewBag.Rol = rol;
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarAprobacion(int id)
        {
            if (!EstaLogueado())
                return Json(new { ok = false, mensaje = "Debe iniciar sesión." });

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.EnviarAprobacionAsync(id, usuario, rol);
            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

        [HttpPost]
        public async Task<IActionResult> Aprobar(int id)
        {
            if (!EstaLogueado())
                return Json(new { ok = false, mensaje = "Debe iniciar sesión." });

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.AprobarAsync(id, usuario, rol);
            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

        [HttpPost]
        public async Task<IActionResult> Devolver(int id, string comentario)
        {
            if (!EstaLogueado())
                return Json(new { ok = false, mensaje = "Debe iniciar sesión." });

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.DevolverAsync(id, comentario, usuario, rol);
            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

        [HttpGet]
        public async Task<IActionResult> Tracking(int id)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var lista = await _solicitudesServicio.ObtenerTrackingAsync(id);
            return Json(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.EliminarAsync(id, usuario, rol);

            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

    }
}
