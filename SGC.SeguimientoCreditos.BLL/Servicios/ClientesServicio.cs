using AutoMapper;
using SGC.SeguimientoCreditos.BLL.Dtos;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.BLL.Servicios
{
    public class ClientesServicio : IClientesServicio
    {
        private readonly IClienteRepositorio _repo;
        private readonly IMapper _mapper;

        public ClientesServicio(IClienteRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }

        public async Task<ClienteDto> ObtenerPorIdAsync(int id)
        {
            var obj = await _repo.ObtenerPorIdAsync(id);
            return _mapper.Map<ClienteDto>(obj);
        }

        public async Task<ClienteDto> CrearAsync(ClienteDto dto)
        {
            var entidad = _mapper.Map<SGC.SeguimientoCreditos.DAL.Entidades.Cliente>(dto);
            await _repo.AgregarAsync(entidad);
            return _mapper.Map<ClienteDto>(entidad);
        }

        public async Task<ClienteDto> ActualizarAsync(int id, ClienteDto dto)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            _mapper.Map(dto, entidad);
            await _repo.ActualizarAsync(entidad);
            return _mapper.Map<ClienteDto>(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}
