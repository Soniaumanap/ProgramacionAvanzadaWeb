using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesServicio _clientesServicio;

        public ClientesController(IClientesServicio clientesServicio)
        {
            _clientesServicio = clientesServicio;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var lista = await _clientesServicio.ListarAsync();
            return Ok(lista); // List<ClienteDto>
        }

        // GET: api/clientes/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var cli = await _clientesServicio.ObtenerAsync(id);
            if (cli == null)
                return NotFound(new { ok = false, mensaje = "Cliente no encontrado." });

            return Ok(cli);
        }

        // GET: api/clientes/identificacion/123456789
        [HttpGet("identificacion/{identificacion}")]
        public async Task<IActionResult> GetPorIdentificacion(string identificacion)
        {
            var cli = await _clientesServicio.ObtenerPorIdentificacionAsync(identificacion);
            if (cli == null)
                return NotFound(new { ok = false, mensaje = "Cliente no encontrado." });

            return Ok(cli);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { ok = false, mensaje = "Datos inválidos." });

            var resp = await _clientesServicio.CrearAsync(dto);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            // Devolvemos solo el estado y el mensaje
            return Ok(new
            {
                ok = true,
                mensaje = resp.Mensaje
            });
        }

        // PUT: api/clientes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { ok = false, mensaje = "Datos inválidos." });

            dto.Id = id;

            var resp = await _clientesServicio.ActualizarAsync(dto);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resp = await _clientesServicio.EliminarAsync(id);

            if (!resp.Ok)
                return BadRequest(new { ok = false, mensaje = resp.Mensaje });

            return Ok(new { ok = true, mensaje = resp.Mensaje });
        }
    }
}
