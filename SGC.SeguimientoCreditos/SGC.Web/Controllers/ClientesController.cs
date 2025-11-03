using Microsoft.AspNetCore.Mvc;
using SGC.Web.Utils;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.Web.Controllers
{
    [RoleAuthorize]
    public class ClientesController : Controller
    {
        private readonly IClientesServicio _svc;
        public ClientesController(IClientesServicio svc) { _svc = svc; }

        public async Task<IActionResult> Index() => View(await _svc.ListarAsync());
        [HttpPost] public async Task<JsonResult> Crear([FromBody] ClienteDto dto) => Json(await _svc.CrearAsync(dto));
        [HttpPost] public async Task<JsonResult> Editar([FromBody] ClienteDto dto) => Json(await _svc.ActualizarAsync(dto));
        [HttpPost] public async Task<JsonResult> Eliminar(int id) => Json(new { ok = await _svc.EliminarAsync(id) });
    }
}
