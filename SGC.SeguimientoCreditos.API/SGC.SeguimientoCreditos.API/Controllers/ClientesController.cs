using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
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
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            var lista = await _clientesServicio.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/clientes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente = await _clientesServicio.ObtenerPorIdAsync(id);
            if (cliente is null) return NotFound();
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDto dto)
        {
            var ok = await _clientesServicio.CrearAsync(dto);
            if (!ok) return BadRequest("No se pudo crear el cliente.");

            // Podrías retornar CreatedAtAction si quieres incluir la URL del recurso
            return Ok();
        }

        // PUT: api/clientes/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClienteDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            var ok = await _clientesServicio.ActualizarAsync(dto);
            if (!ok) return NotFound("Cliente no encontrado.");

            return Ok();
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _clientesServicio.EliminarAsync(id);
            if (!ok) return NotFound("Cliente no encontrado.");

            return Ok();
        }
    }
}
