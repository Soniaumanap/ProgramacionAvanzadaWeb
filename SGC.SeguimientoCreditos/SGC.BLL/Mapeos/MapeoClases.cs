using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGC.BLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<SolicitudCredito, SolicitudDto>().ReverseMap();
        }
    }
}
