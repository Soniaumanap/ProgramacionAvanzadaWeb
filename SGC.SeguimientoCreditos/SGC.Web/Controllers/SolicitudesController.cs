using Microsoft.AspNetCore.Mvc;
using SGC.Web.Utils;
using SGC.BLL.Servicios;
using SGC.DAL.Entidades;

namespace SGC.Web.Controllers
{
    [RoleAuthorize]
    public class SolicitudesController : Controller
    {
        private readonly ISolicitudesServicio _svc;
        public SolicitudesController(ISolicitudesServicio svc) { _svc = svc; }

        public async Task<IActionResult> Index()
        {
            var rol = Enum.Parse<Rol>(HttpContext.Session.GetString("Rol")!);
            var items = await _svc.ListarPorRolAsync(rol);
            return View(items);
        }

        [RoleAuthorize(Rol.ServicioCliente, Rol.Admin)]
        public IActionResult Crear() => View();

        [HttpPost]
        [RoleAuthorize(Rol.ServicioCliente, Rol.Admin)]
        public async Task<JsonResult> CrearPost(int clienteId, string identificacion, decimal monto, string comentarios)
        {
            var nombre = HttpContext.Session.GetString("Nombre")!;
            var rol = Enum.Parse<Rol>(HttpContext.Session.GetString("Rol")!);
            return Json(await _svc.CrearAsync(clienteId, identificacion, monto, comentarios, nombre, rol));
        }

        [HttpPost]
        public async Task<JsonResult> EnviarAprobacion(int id)
        {
            var nombre = HttpContext.Session.GetString("Nombre")!;
            var rol = Enum.Parse<Rol>(HttpContext.Session.GetString("Rol")!);
            return Json(await _svc.EnviarAprobacionAsync(id, nombre, rol));
        }

        [HttpPost]
        public async Task<JsonResult> Aprobar(int id)
        {
            var nombre = HttpContext.Session.GetString("Nombre")!;
            var rol = Enum.Parse<Rol>(HttpContext.Session.GetString("Rol")!);
            return Json(await _svc.AprobarAsync(id, nombre, rol));
        }

        [HttpPost]
        public async Task<JsonResult> Devolver(int id, string comentario)
        {
            var nombre = HttpContext.Session.GetString("Nombre")!;
            var rol = Enum.Parse<Rol>(HttpContext.Session.GetString("Rol")!);
            return Json(await _svc.DevolverAsync(id, comentario, nombre, rol));
        }
    }
}
