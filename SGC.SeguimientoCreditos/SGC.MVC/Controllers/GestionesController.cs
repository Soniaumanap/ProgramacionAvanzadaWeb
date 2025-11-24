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

        private bool EstaLogueado()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"));
        }

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
                return Unauthorized();

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.EnviarAprobacionAsync(id, usuario, rol);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Aprobar(int id)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.AprobarAsync(id, usuario, rol);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Devolver(int id, string comentario)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var usuario = HttpContext.Session.GetString("UsuarioNombre") ?? "";
            var rol = HttpContext.Session.GetString("UsuarioRol") ?? "";

            var resp = await _solicitudesServicio.DevolverAsync(id, comentario, usuario, rol);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Tracking(int id)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var lista = await _solicitudesServicio.ObtenerTrackingAsync(id);
            return Json(lista);
        }
    }
}
