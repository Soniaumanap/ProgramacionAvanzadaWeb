using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServicio _service;

        public UsuariosController(IUsuariosServicio service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string correo, string contrasena)
        {
            var usuario = await _service.Login(correo, contrasena);
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.ObtenerTodosAsync());
    }
}
