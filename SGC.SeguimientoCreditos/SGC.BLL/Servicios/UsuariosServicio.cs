using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.BLL.Servicios
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

        public async Task<CustomResponse<UsuarioDto>> RegistrarAsync(UsuarioDto dto)
        {
            var existeIdent = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (existeIdent != null)
                return CustomResponse<UsuarioDto>.Fail("Identificación ya registrada.");

            var existeEmail = await _repo.ObtenerPorEmailAsync(dto.Email);
            if (existeEmail != null)
                return CustomResponse<UsuarioDto>.Fail("Correo electrónico ya registrado.");

            var entity = _mapper.Map<Usuario>(dto);

            var ok = await _repo.AgregarAsync(entity);
            if (!ok)
                return CustomResponse<UsuarioDto>.Fail("No se pudo registrar el usuario.");

            dto.Id = entity.Id;

            return CustomResponse<UsuarioDto>.Success(dto, "Usuario registrado correctamente.");
        }

        public async Task<UsuarioDto?> LoginAsync(string email, string password)
        {
            var entity = await _repo.ObtenerPorEmailAsync(email);
            if (entity == null || entity.Password != password || !entity.Activo)
                return null;

            return _mapper.Map<UsuarioDto>(entity);
        }

        public async Task<List<UsuarioDto>> ListarAsync()
        {
            var lista = await _repo.ListarAsync();
            return _mapper.Map<List<UsuarioDto>>(lista);
        }

        public async Task<UsuarioDto?> ObtenerAsync(int id)
        {
            var entity = await _repo.ObtenerAsync(id);
            return _mapper.Map<UsuarioDto>(entity);
        }

        public async Task<CustomResponse<bool>> ActualizarAsync(UsuarioDto dto)
        {
            var entity = await _repo.ObtenerAsync(dto.Id);
            if (entity == null)
                return CustomResponse<bool>.Fail("Usuario no encontrado.");

            _mapper.Map(dto, entity);

            var ok = await _repo.ActualizarAsync(entity);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo actualizar el usuario.");

            return CustomResponse<bool>.Success(true, "Usuario actualizado correctamente.");
        }

        public async Task<CustomResponse<bool>> EliminarAsync(int id)
        {
            var ok = await _repo.EliminarAsync(id);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo eliminar el usuario.");

            return CustomResponse<bool>.Success(true, "Usuario eliminado correctamente.");
        }
    }
}
