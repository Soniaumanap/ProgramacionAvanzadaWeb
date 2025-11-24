using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesServicio _clientes;

        public ClientesController(IClientesServicio clientes)
        {
            _clientes = clientes;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _clientes.ListarAsync();
            return Ok(lista);
        }

        // GET: api/clientes/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var c = await _clientes.ObtenerAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        // GET: api/clientes/identificacion/115500000
        [HttpGet("identificacion/{identificacion}")]
        public async Task<IActionResult> GetPorIdentificacion(string identificacion)
        {
            var c = await _clientes.ObtenerPorIdentificacionAsync(identificacion);
            if (c == null) return NotFound();
            return Ok(c);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto dto)
        {
            var resp = await _clientes.CrearAsync(dto);
            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // PUT: api/clientes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDto dto)
        {
            dto.Id = id;
            var resp = await _clientes.ActualizarAsync(dto);
            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _clientes.EliminarAsync(id);
            if (!resp.Ok)
                return BadRequest(resp);

            return Ok(resp);
        }
    }
}
