using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;
using SGC.BLL.Dtos;

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

        // GET: /Reporte/Index?id=11550
        public async Task<IActionResult> Index(int? id)
        {
            if (!EstaLogueado())
                return RedirectToAction("Login", "Auth");

            var lista = new List<TrackingDto>();

            if (id.HasValue)
            {
                lista = await _solicitudesServicio.ObtenerTrackingAsync(id.Value);
                ViewBag.GestionId = id.Value;
            }
            else
            {
                ViewBag.GestionId = null;
            }

            return View(lista);
        }
    }
}
