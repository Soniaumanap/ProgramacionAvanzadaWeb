using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CLIENTE
            CreateMap<Cliente, ClienteDto>().ReverseMap();

            // USUARIO
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            // SOLICITUD CREDITO
            CreateMap<SolicitudCredito, SolicitudCreditoDto>().ReverseMap();

            // TRACKING GESTION
            CreateMap<TrackingGestion, TrackingGestionDto>().ReverseMap();
        }
    }
}
