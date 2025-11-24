using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingsController : ControllerBase
    {
        private readonly ITrackingsServicio _trackingsServicio;

        public TrackingsController(ITrackingsServicio trackingsServicio)
        {
            _trackingsServicio = trackingsServicio;
        }

        // GET: api/trackings/gestion/
        [HttpGet("gestion/{gestionId:int}")]
        public async Task<ActionResult<List<TrackingGestionDto>>> GetPorGestion(int gestionId)
        {
            var lista = await _trackingsServicio.ObtenerPorGestionAsync(gestionId);
            return Ok(lista);
        }

        // POST: api/trackings
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TrackingGestionDto dto)
        {
            await _trackingsServicio.CrearAsync(dto);
            return Ok();
        }
    }
}
