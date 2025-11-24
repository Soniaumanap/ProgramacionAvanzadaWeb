using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;
using SGC.BLL.Dtos;

namespace SGC.MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClientesServicio _clientesServicio;

        public ClientesController(IClientesServicio clientesServicio)
        {
            _clientesServicio = clientesServicio;
        }

        private bool EstaLogueado()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"));
        }

        public async Task<IActionResult> Index()
        {
            if (!EstaLogueado())
                return RedirectToAction("Login", "Auth");

            var lista = await _clientesServicio.ListarAsync();
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteDto dto)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var resp = await _clientesServicio.CrearAsync(dto);
            return Json(resp);
        }

        public async Task<IActionResult> Get(int id)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var cliente = await _clientesServicio.ObtenerAsync(id);
            return Json(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteDto dto)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var resp = await _clientesServicio.ActualizarAsync(dto);
            return Json(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!EstaLogueado())
                return Unauthorized();

            var resp = await _clientesServicio.EliminarAsync(id);
            return Json(resp);
        }
    }
}
