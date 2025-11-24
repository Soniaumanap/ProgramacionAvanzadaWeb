using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServicio _usuariosServicio;

        public UsuariosController(IUsuariosServicio usuariosServicio)
        {
            _usuariosServicio = usuariosServicio;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> Get()
        {
            var lista = await _usuariosServicio.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/usuarios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> Get(int id)
        {
            var usuario = await _usuariosServicio.ObtenerPorIdAsync(id);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioDto dto)
        {
            var ok = await _usuariosServicio.CrearAsync(dto);
            if (!ok) return BadRequest("No se pudo crear el usuario.");
            return Ok();
        }

        // PUT: api/usuarios/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            var ok = await _usuariosServicio.ActualizarAsync(dto);
            if (!ok) return NotFound("Usuario no encontrado.");
            return Ok();
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _usuariosServicio.EliminarAsync(id);
            if (!ok) return NotFound("Usuario no encontrado.");
            return Ok();
        }
    }
}
