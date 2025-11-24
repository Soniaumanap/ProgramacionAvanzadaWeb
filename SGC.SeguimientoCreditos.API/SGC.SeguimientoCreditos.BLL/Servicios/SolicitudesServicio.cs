using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class SolicitudesServicio : ISolicitudesServicio
    {
        private readonly ISolicitudesRepositorio _repo;
        private readonly IMapper _mapper;

        public SolicitudesServicio(ISolicitudesRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SolicitudCreditoDto>> ObtenerTodasAsync()
        {
            var lista = await _repo.ObtenerTodasAsync();
            return _mapper.Map<List<SolicitudCreditoDto>>(lista);
        }

        public async Task<SolicitudCreditoDto?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            return _mapper.Map<SolicitudCreditoDto?>(entidad);
        }

        public async Task<bool> CrearAsync(SolicitudCreditoDto dto)
        {
            var entidad = _mapper.Map<SolicitudCredito>(dto);
            return await _repo.AgregarAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(SolicitudCreditoDto dto)
        {
            var entidad = _mapper.Map<SolicitudCredito>(dto);
            return await _repo.ActualizarAsync(entidad);
        }
    }
}
