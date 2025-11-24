using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.BLL.Servicios
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

        public async Task<CustomResponse<ClienteDto>> CrearAsync(ClienteDto dto)
        {
            var existe = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (existe != null)
                return CustomResponse<ClienteDto>.Fail("Ya existe un cliente con esa identificación.");

            var ent = _mapper.Map<Cliente>(dto);

            var ok = await _repo.AgregarAsync(ent);
            if (!ok)
                return CustomResponse<ClienteDto>.Fail("No se pudo registrar el cliente.");

            dto.Id = ent.Id;

            return CustomResponse<ClienteDto>.Success(dto, "Cliente registrado correctamente.");
        }

        public async Task<CustomResponse<bool>> ActualizarAsync(ClienteDto dto)
        {
            var ent = await _repo.ObtenerAsync(dto.Id);
            if (ent == null)
                return CustomResponse<bool>.Fail("Cliente no encontrado.");

            _mapper.Map(dto, ent);

            var ok = await _repo.ActualizarAsync(ent);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo actualizar el cliente.");

            return CustomResponse<bool>.Success(true, "Cliente actualizado correctamente.");
        }

        public async Task<CustomResponse<bool>> EliminarAsync(int id)
        {
            var ok = await _repo.EliminarAsync(id);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo eliminar el cliente.");

            return CustomResponse<bool>.Success(true, "Cliente eliminado correctamente.");
        }

        public async Task<List<ClienteDto>> ListarAsync()
        {
            var lista = await _repo.ListarAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }

        public async Task<ClienteDto?> ObtenerAsync(int id)
        {
            var entity = await _repo.ObtenerAsync(id);
            return _mapper.Map<ClienteDto>(entity);
        }

        public async Task<ClienteDto?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var entity = await _repo.ObtenerPorIdentificacionAsync(identificacion);
            return _mapper.Map<ClienteDto>(entity);
        }
    }
}
