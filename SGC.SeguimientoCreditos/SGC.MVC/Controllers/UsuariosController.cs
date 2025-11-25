using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _usuariosServicio;

        public UsuariosController(IUsuariosServicio usuariosServicio)
        {
            _usuariosServicio = usuariosServicio;
        }

        private bool EsAdmin()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            return rol == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Auth");

            var lista = await _usuariosServicio.ListarAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(int id)
        {
            if (!EsAdmin())
                return Unauthorized();

            var usuario = await _usuariosServicio.ObtenerAsync(id);
            if (usuario == null)
                return NotFound();

            return Json(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string identificacion,
                                              string nombre,
                                              string email,
                                              string password,
                                              string rol,
                                              bool activo)
        {
            if (!EsAdmin())
                return Json(new { ok = false, mensaje = "No tiene permisos para esta acción." });

            var dto = new UsuarioDto
            {
                Identificacion = identificacion,
                Nombre = nombre,
                Email = email,
                Password = password,
                Rol = rol,
                Activo = activo
            };

            var resp = await _usuariosServicio.CrearAsync(dto);

            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id,
                                               string identificacion,
                                               string nombre,
                                               string email,
                                               string rol,
                                               bool activo)
        {
            if (!EsAdmin())
                return Json(new { ok = false, mensaje = "No tiene permisos para esta acción." });

            var dto = new UsuarioDto
            {
                Id = id,
                Identificacion = identificacion,
                Nombre = nombre,
                Email = email,
                Rol = rol,
                Activo = activo
                // Password se mantiene igual, no se cambia aquí
            };

            var resp = await _usuariosServicio.ActualizarAsync(dto);

            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!EsAdmin())
                return Json(new { ok = false, mensaje = "No tiene permisos para esta acción." });

            var resp = await _usuariosServicio.EliminarAsync(id);

            return Json(new { ok = resp.Ok, mensaje = resp.Mensaje });
        }
    }
}
