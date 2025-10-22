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
        }
    }
}
