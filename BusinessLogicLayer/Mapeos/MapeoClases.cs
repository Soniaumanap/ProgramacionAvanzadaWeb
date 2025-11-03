using AutoMapper;
using BusinessLogicLayer.Dtos;
using DataAccesssLayer.Entidades;
namespace BusinessLogicLayer.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();

            CreateMap<DocumentoSolicitud, DocumentoSolicitudDto>();
            CreateMap<DocumentoSolicitudDto, DocumentoSolicitud>();

            CreateMap<EstadoSolicitud, EstadoSolicitudDto>();
            CreateMap<EstadoSolicitudDto, EstadoSolicitud>();

            CreateMap<HistorialSolicitud, HistorialSolicitudDto>();
            CreateMap<HistorialSolicitudDto, HistorialSolicitud>();

            CreateMap<Rol, RolDto>();
            CreateMap<RolDto, Rol>();

            CreateMap<SolicitudCredito, SolicitudCreditoDto>();
            CreateMap<SolicitudCreditoDto, SolicitudCredito>();
        }
    }
}
