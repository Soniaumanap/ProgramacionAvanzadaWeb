using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServicio _usuarios;

        public UsuariosController(IUsuariosServicio usuarios)
        {
            _usuarios = usuarios;
        }

        // POST: api/usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _usuarios.LoginAsync(request.Email, request.Password);
            if (user == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(user);
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _usuarios.ListarAsync();
            return Ok(lista);
        }

        // GET: api/usuarios/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _usuarios.ObtenerAsync(id);
            if (u == null) return NotFound();

            return Ok(u);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDto dto)
        {
            var resp = await _usuarios.RegistrarAsync(dto);
            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDto dto)
        {
            dto.Id = id;
            var resp = await _usuarios.ActualizarAsync(dto);

            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _usuarios.EliminarAsync(id);
            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
