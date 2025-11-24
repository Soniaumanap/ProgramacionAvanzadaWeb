using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;

namespace SGC.MVC.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ISolicitudesServicio _solicitudesServicio;

        public ReporteController(ISolicitudesServicio solicitudesServicio)
        {
            _solicitudesServicio = solicitudesServicio;
        }

        private bool EstaLogueado()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"));
        }

        public IActionResult Index()
        {
            if (!EstaLogueado())
                return RedirectToAction("Login", "Auth");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(int gestionId)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var lista = await _solicitudesServicio.ObtenerTrackingAsync(gestionId);
            return Json(lista);
        }
    }
}
