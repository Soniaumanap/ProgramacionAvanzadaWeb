using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;
using SGC.BLL.Dtos;

namespace SGC.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _usuarios;

        public UsuariosController(IUsuariosServicio usuarios)
        {
            _usuarios = usuarios;
        }

        private bool EsAdmin()
        {
            return HttpContext.Session.GetString("UsuarioRol") == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Auth");

            var lista = await _usuarios.ListarAsync();
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto dto)
        {
            if (!EsAdmin())
                return Unauthorized();

            var resp = await _usuarios.RegistrarAsync(dto);
            return Json(resp);
        }

        public async Task<IActionResult> Get(int id)
        {
            if (!EsAdmin())
                return Unauthorized();

            var usuario = await _usuarios.ObtenerAsync(id);
            return Json(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsuarioDto dto)
        {
            if (!EsAdmin())
                return Unauthorized();

            var resp = await _usuarios.ActualizarAsync(dto);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!EsAdmin())
                return Unauthorized();

            var resp = await _usuarios.EliminarAsync(id);
            return Json(resp);
        }
    }
}
