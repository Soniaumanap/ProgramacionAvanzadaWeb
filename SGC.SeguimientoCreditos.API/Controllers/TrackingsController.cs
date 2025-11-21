using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingsController : ControllerBase
    {
        private readonly ITrackingsServicio _service;

        public TrackingsController(ITrackingsServicio service)
        {
            _service = service;
        }

        [HttpGet("solicitud/{solicitudId}")]
        public async Task<ActionResult> GetBySolicitud(int solicitudId) =>
            Ok(await _service.ObtenerPorSolicitudAsync(solicitudId));

        [HttpPost]
        public async Task<ActionResult> Create(TrackingDto dto) =>
            Ok(await _service.CrearAsync(dto));
    }
}
