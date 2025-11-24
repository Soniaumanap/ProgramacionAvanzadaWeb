using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudesServicio _solicitudesServicio;

        public SolicitudesController(ISolicitudesServicio solicitudesServicio)
        {
            _solicitudesServicio = solicitudesServicio;
        }

        // GET: api/solicitudes
        [HttpGet]
        public async Task<ActionResult<List<SolicitudCreditoDto>>> Get()
        {
            var lista = await _solicitudesServicio.ObtenerTodasAsync();
            return Ok(lista);
        }

        // GET: api/solicitudes/11550
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SolicitudCreditoDto>> Get(int id)
        {
            var solicitud = await _solicitudesServicio.ObtenerPorIdAsync(id);
            if (solicitud is null) return NotFound();
            return Ok(solicitud);
        }

        // POST: api/solicitudes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SolicitudCreditoDto dto)
        {
            var ok = await _solicitudesServicio.CrearAsync(dto);
            if (!ok) return BadRequest("No se pudo crear la solicitud.");
            return Ok();
        }

        // PUT: api/solicitudes/11550
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SolicitudCreditoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            var ok = await _solicitudesServicio.ActualizarAsync(dto);
            if (!ok) return NotFound("Solicitud no encontrada.");
            return Ok();
        }
    }
}
