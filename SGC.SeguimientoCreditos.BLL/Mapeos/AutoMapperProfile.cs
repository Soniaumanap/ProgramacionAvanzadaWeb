using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.DAL.Entidades;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGC.SeguimientoCreditos.BLL.Mapeos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<SolicitudCredito, SolicitudDto>().ReverseMap();
            CreateMap<TrackingGestion, TrackingDto>().ReverseMap();
        }
    }
}
