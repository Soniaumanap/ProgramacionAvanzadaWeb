using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly IUsuarioRepositorio _repo;
        private readonly IMapper _mapper;

        public UsuariosServicio(IUsuarioRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Login(string correo, string contrasena)
        {
            var usuario = await _repo.Login(correo, contrasena);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return _mapper.Map<List<UsuarioDto>>(lista);
        }
    }
}
