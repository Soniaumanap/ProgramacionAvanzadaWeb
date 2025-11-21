using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class SolicitudesServicio : ISolicitudesServicio
    {
        private readonly ISolicitudCreditoRepositorio _repo;
        private readonly IMapper _mapper;

        public SolicitudesServicio(ISolicitudCreditoRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SolicitudDto>> ObtenerTodosAsync()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return _mapper.Map<List<SolicitudDto>>(lista);
        }

        public async Task<SolicitudDto> ObtenerPorIdAsync(int id)
        {
            var obj = await _repo.ObtenerPorIdAsync(id);
            return _mapper.Map<SolicitudDto>(obj);
        }

        public async Task<SolicitudDto> CrearAsync(SolicitudDto dto)
        {
            var entidad = _mapper.Map<SGC.SeguimientoCreditos.DAL.Entidades.SolicitudCredito>(dto);
            await _repo.AgregarAsync(entidad);
            return _mapper.Map<SolicitudDto>(entidad);
        }

        public async Task<SolicitudDto> ActualizarAsync(int id, SolicitudDto dto)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            _mapper.Map(dto, entidad);
            await _repo.ActualizarAsync(entidad);
            return _mapper.Map<SolicitudDto>(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}
