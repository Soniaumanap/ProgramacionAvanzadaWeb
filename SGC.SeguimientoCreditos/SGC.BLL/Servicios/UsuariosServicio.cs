using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
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

        // ===================== LOGIN =====================
        public async Task<UsuarioDto?> LoginAsync(string email, string password)
        {
            // Buscar usuario por email
            var usuario = await _repo.ObtenerPorEmailAsync(email);
            if (usuario == null)
                return null;

            // Validar contraseña (sin hashing por ahora) y que esté activo
            if (usuario.Password != password || !usuario.Activo)
                return null;

            // Mapear a DTO y devolver
            return _mapper.Map<UsuarioDto>(usuario);
        }

        // ===================== REGISTRAR (Self-Register / Auth) =====================
        public async Task<CustomResponse<UsuarioDto>> RegistrarAsync(UsuarioDto dto)
        {
            // Rol por defecto cuando se registra desde el formulario público
            if (string.IsNullOrWhiteSpace(dto.Rol))
            {
                dto.Rol = "ServicioCliente";   // ← cámbialo si quieres otro rol por defecto
            }

            // Validar identificación duplicada
            var existeIdent = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (existeIdent != null)
                return CustomResponse<UsuarioDto>.Fail("La identificación ya está registrada.");

            // Validar email duplicado
            var existeEmail = await _repo.ObtenerPorEmailAsync(dto.Email);
            if (existeEmail != null)
                return CustomResponse<UsuarioDto>.Fail("El correo electrónico ya está registrado.");

            // Mapear a entidad
            var entity = _mapper.Map<Usuario>(dto);

            var ok = await _repo.AgregarAsync(entity);
            if (!ok)
                return CustomResponse<UsuarioDto>.Fail("No se pudo registrar el usuario.");

            dto.Id = entity.Id;

            return CustomResponse<UsuarioDto>.Success(dto, "Usuario registrado correctamente.");
        }


        // ===================== LISTAR =====================
        public async Task<List<UsuarioDto>> ListarAsync()
        {
            var lista = await _repo.ListarAsync();
            return _mapper.Map<List<UsuarioDto>>(lista);
        }

        // ===================== OBTENER POR ID =====================
        public async Task<UsuarioDto?> ObtenerAsync(int id)
        {
            var entity = await _repo.ObtenerAsync(id);
            if (entity == null) return null;

            return _mapper.Map<UsuarioDto>(entity);
        }

        // ===================== CREAR (Admin - módulo Administración de usuarios) =====================
        public async Task<CustomResponse<UsuarioDto>> CrearAsync(UsuarioDto dto)
        {
            // Validar identificación duplicada
            var existeIdentificacion = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (existeIdentificacion != null)
            {
                return CustomResponse<UsuarioDto>.Fail("La identificación ya está registrada.");
            }

            // Validar email duplicado
            var existeEmail = await _repo.ObtenerPorEmailAsync(dto.Email);
            if (existeEmail != null)
            {
                return CustomResponse<UsuarioDto>.Fail("El email ya está registrado.");
            }

            // Convertir DTO a entidad
            var entidad = _mapper.Map<Usuario>(dto);

            // Guardar en BD
            var ok = await _repo.AgregarAsync(entidad);
            if (!ok)
            {
                return CustomResponse<UsuarioDto>.Fail("No se pudo registrar el usuario.");
            }

            // Asignar el ID generado a dto
            dto.Id = entidad.Id;

            return CustomResponse<UsuarioDto>.Success(dto, "Usuario registrado correctamente.");
        }

        // ===================== ACTUALIZAR =====================
        public async Task<CustomResponse<UsuarioDto>> ActualizarAsync(UsuarioDto dto)
        {
            // 1. Buscar el usuario existente
            var existente = await _repo.ObtenerAsync(dto.Id);
            if (existente == null)
            {
                return CustomResponse<UsuarioDto>.Fail("El usuario no existe.");
            }

            // 2. Validar identificación duplicada en otro usuario
            var porIdentificacion = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (porIdentificacion != null && porIdentificacion.Id != dto.Id)
            {
                return CustomResponse<UsuarioDto>.Fail("La identificación ya está registrada en otro usuario.");
            }

            // 3. Validar email duplicado en otro usuario
            var porEmail = await _repo.ObtenerPorEmailAsync(dto.Email);
            if (porEmail != null && porEmail.Id != dto.Id)
            {
                return CustomResponse<UsuarioDto>.Fail("El email ya está registrado en otro usuario.");
            }

            // 4. Actualizar campos (sin tocar la contraseña aquí)
            existente.Identificacion = dto.Identificacion;
            existente.Nombre = dto.Nombre;
            existente.Email = dto.Email;
            existente.Rol = dto.Rol;
            existente.Activo = dto.Activo;

            var ok = await _repo.ActualizarAsync(existente);
            if (!ok)
            {
                return CustomResponse<UsuarioDto>.Fail("No se pudo actualizar el usuario.");
            }

            // 5. Devolver el dto actualizado
            var actualizado = _mapper.Map<UsuarioDto>(existente);
            return CustomResponse<UsuarioDto>.Success(actualizado, "Usuario actualizado correctamente.");
        }

        // ===================== ELIMINAR =====================
        public async Task<CustomResponse<bool>> EliminarAsync(int id)
        {
            var ok = await _repo.EliminarAsync(id);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo eliminar el usuario.");

            return CustomResponse<bool>.Success(true, "Usuario eliminado correctamente.");
        }
    }
}
