using Microsoft.AspNetCore.Mvc;
using SGC.Web.Utils;
using SGC.BLL.Servicios;

namespace SGC.Web.Controllers
{
    [RoleAuthorize]
    public class ReportesController : Controller
    {
        private readonly ISolicitudesServicio _svc;
        public ReportesController(ISolicitudesServicio svc) { _svc = svc; }
        public IActionResult Tracking() => View();
        [HttpGet] public async Task<JsonResult> Buscar(int gestionId) => Json(await _svc.ObtenerTrackingAsync(gestionId));
    }
}
