using Microsoft.AspNetCore.Mvc;
using SGC.Web.Utils;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;
using SGC.DAL.Entidades;

namespace SGC.Web.Controllers
{
    [RoleAuthorize(Rol.Admin)]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _svc;
        public UsuariosController(IUsuariosServicio svc) { _svc = svc; }

        public async Task<IActionResult> Index() => View(await _svc.ListarAsync());
        [HttpPost] public async Task<JsonResult> Crear([FromBody] UsuarioDto dto) => Json(await _svc.CrearAsync(dto));
        [HttpPost] public async Task<JsonResult> Editar([FromBody] UsuarioDto dto) => Json(await _svc.ActualizarAsync(dto));
        [HttpPost] public async Task<JsonResult> Eliminar(int id) => Json(new { ok = await _svc.EliminarAsync(id) });
    }
}
