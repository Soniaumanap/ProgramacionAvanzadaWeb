using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;

namespace SGC.BLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            // Usuario
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            // Cliente
            CreateMap<Cliente, ClienteDto>().ReverseMap();

            // SolicitudCredito
            CreateMap<SolicitudCredito, SolicitudDto>().ReverseMap();

            // TrackingGestion
            CreateMap<TrackingGestion, TrackingDto>().ReverseMap();
        }
    }
}
