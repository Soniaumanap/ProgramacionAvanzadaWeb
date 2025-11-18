using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class TrackingsServicio : ITrackingsServicio
    {
        private readonly ITrackingGestionRepositorio _repo;
        private readonly IMapper _mapper;

        public TrackingsServicio(ITrackingGestionRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TrackingDto>> ObtenerPorSolicitudAsync(int solicitudId)
        {
            var lista = await _repo.ObtenerPorSolicitud(solicitudId);
            return _mapper.Map<List<TrackingDto>>(lista);
        }

        public async Task<TrackingDto> CrearAsync(TrackingDto dto)
        {
            var entidad = _mapper.Map<SGC.SeguimientoCreditos.DAL.Entidades.TrackingGestion>(dto);
            await _repo.AgregarAsync(entidad);
            return _mapper.Map<TrackingDto>(entidad);
        }
    }
}
