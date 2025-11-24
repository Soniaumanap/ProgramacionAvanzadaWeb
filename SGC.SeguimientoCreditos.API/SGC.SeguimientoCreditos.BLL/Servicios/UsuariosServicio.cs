using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly IUsuariosRepositorio _repo;
        private readonly IMapper _mapper;

        public UsuariosServicio(IUsuariosRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var lista = await _repo.ObtenerUsuariosAsync();
            return _mapper.Map<List<UsuarioDto>>(lista);
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            return _mapper.Map<UsuarioDto?>(entidad);
        }

        public async Task<UsuarioDto?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var entidad = await _repo.ObtenerPorIdentificacionAsync(identificacion);
            return _mapper.Map<UsuarioDto?>(entidad);
        }

        public async Task<bool> CrearAsync(UsuarioDto dto)
        {
            var entidad = _mapper.Map<Usuario>(dto);
            return await _repo.AgregarAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(UsuarioDto dto)
        {
            var entidad = _mapper.Map<Usuario>(dto);
            return await _repo.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}
