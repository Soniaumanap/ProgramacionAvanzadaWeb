using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServicio _servicio;

        public UsuariosController(IUsuariosServicio servicio)
        {
            _servicio = servicio;
        }

        // ================== LISTAR ==================
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _servicio.ListarAsync();
            return Ok(lista);
        }

        // ================== OBTENER POR ID ==================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var usuario = await _servicio.ObtenerAsync(id);
            if (usuario == null)
                return NotFound(new { ok = false, mensaje = "Usuario no encontrado." });

            return Ok(usuario);
        }

        // ================== CREAR ==================
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioDto dto)
        {
            var resp = await _servicio.CrearAsync(dto);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new
            {
                ok = true,
                mensaje = resp.Mensaje,
                usuario = resp // <- incluye el DTO con el nuevo Id
            });
        }

        // ================== ACTUALIZAR ==================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UsuarioDto dto)
        {
            dto.Id = id;

            var resp = await _servicio.ActualizarAsync(dto);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        // ================== ELIMINAR ==================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resp = await _servicio.EliminarAsync(id);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        // ================== LOGIN (Opcional API) ==================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var usuario = await _servicio.LoginAsync(model.Email, model.Password);

            if (usuario == null)
                return Unauthorized(new { ok = false, mensaje = "Credenciales incorrectas" });

            return Ok(new { ok = true, usuario });
        }
    }

    // DTO de Login para API
    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
