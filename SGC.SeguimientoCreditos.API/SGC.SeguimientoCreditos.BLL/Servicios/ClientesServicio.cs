using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class ClientesServicio : IClientesServicio
    {
        private readonly IClientesRepositorio _repo;
        private readonly IMapper _mapper;

        public ClientesServicio(IClientesRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            return _mapper.Map<ClienteDto?>(entidad);
        }

        public async Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var entidad = await _repo.ObtenerPorIdentificacionAsync(identificacion);
            return _mapper.Map<ClienteDto?>(entidad);
        }

        public async Task<bool> CrearAsync(ClienteDto dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);
            return await _repo.AgregarAsync(entidad);
        }

        public async Task<bool> ActualizarAsync(ClienteDto dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);
            return await _repo.ActualizarAsync(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}
