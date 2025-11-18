using Microsoft.AspNetCore.Mvc;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;

namespace SGC.SeguimientoCreditos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesServicio _service;

        public ClientesController(IClientesServicio service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) =>
            Ok(await _service.ObtenerPorIdAsync(id));

        [HttpPost]
        public async Task<ActionResult> Create(ClienteDto dto) =>
            Ok(await _service.CrearAsync(dto));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ClienteDto dto) =>
            Ok(await _service.ActualizarAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) =>
            Ok(await _service.EliminarAsync(id));
    }
}
