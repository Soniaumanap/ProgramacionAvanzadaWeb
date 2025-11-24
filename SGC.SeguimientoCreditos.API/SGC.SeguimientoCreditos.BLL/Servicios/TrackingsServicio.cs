using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class TrackingsServicio : ITrackingsServicio
    {
        private readonly ITrackingsRepositorio _repo;
        private readonly IMapper _mapper;

        public TrackingsServicio(ITrackingsRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TrackingGestionDto>> ObtenerPorGestionAsync(int gestionId)
        {
            var lista = await _repo.ObtenerPorGestionAsync(gestionId);
            return _mapper.Map<List<TrackingGestionDto>>(lista);
        }

        public async Task CrearAsync(TrackingGestionDto dto)
        {
            var entidad = _mapper.Map<TrackingGestion>(dto);
            await _repo.AgregarAsync(entidad);
        }
    }
}
