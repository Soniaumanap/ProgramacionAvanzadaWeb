using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios;

namespace SGC.BLL.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly IUsuariosRepositorio _repo; private readonly IMapper _mapper;
        public UsuariosServicio(IUsuariosRepositorio repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<CustomResponse<UsuarioDto>> CrearAsync(UsuarioDto dto)
        {
            var ent = _mapper.Map<Usuario>(dto);

            if (await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion) != null)
            {
                return CustomResponse<UsuarioDto>.Fail("Identificación ya registrada.");
            }
            
            if (!await _repo.AgregarAsync(ent))
            {
                return CustomResponse<UsuarioDto>.Fail("Usuario no se pudo guardar.");
            }
            dto.Id = ent.Id;
            return CustomResponse<UsuarioDto>.Ok(dto);
        }

        public async Task<CustomResponse<UsuarioDto>> ActualizarAsync(UsuarioDto dto)
            => (await _repo.ActualizarAsync(_mapper.Map<Usuario>(dto))) ? CustomResponse<UsuarioDto>.Ok(dto) : CustomResponse<UsuarioDto>.Fail("No se pudo actualizar.");
        public Task<bool> EliminarAsync(int id) => _repo.EliminarAsync(id);
        public async Task<List<UsuarioDto>> ListarAsync() => (await _repo.ObtenerUsuariosAsync()).Select(_mapper.Map<UsuarioDto>).ToList();
        public async Task<UsuarioDto?> BuscarPorEmailPassAsync(string email, string pass)
        {
            var u = (await _repo.ObtenerUsuariosAsync())
                .FirstOrDefault(x => x.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) && x.Password == pass && x.Activo);
            return u is null ? null : _mapper.Map<UsuarioDto>(u);
        }

        public async Task<CustomResponse<UsuarioDto>> RegistrarAsync(RegisterDto dto)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Identificacion) || string.IsNullOrWhiteSpace(dto.Nombre))
                return CustomResponse<UsuarioDto>.Fail("Todos los campos son obligatorios.");

            if (dto.Password.Length < 4)
                return CustomResponse<UsuarioDto>.Fail("La contraseña debe tener al menos 4 caracteres (v1 demo).");

            var existentes = await _repo.ObtenerUsuariosAsync();
            if (existentes.Any(x => x.Email.Equals(dto.Email, System.StringComparison.OrdinalIgnoreCase)))
                return CustomResponse<UsuarioDto>.Fail("El email ya está registrado.");
            if (existentes.Any(x => x.Identificacion == dto.Identificacion))
                return CustomResponse<UsuarioDto>.Fail("La identificación ya está registrada.");

            var nuevo = new SGC.DAL.Entidades.Usuario
            {
                Identificacion = dto.Identificacion,
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                Rol = SGC.DAL.Entidades.Rol.ServicioCliente, // rol por defecto
                Activo = true
            };

            await _repo.AgregarAsync(nuevo);

            var dtoCreado = _mapper.Map<UsuarioDto>(nuevo);
            return CustomResponse<UsuarioDto>.Ok(dtoCreado, "Usuario registrado con éxito.");
        }

    }

    public class ClientesServicio : IClientesServicio
    {
        private readonly IClientesRepositorio _repo; private readonly IMapper _mapper;
        public ClientesServicio(IClientesRepositorio repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<CustomResponse<ClienteDto>> CrearAsync(ClienteDto dto)
        {
            if (await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion) != null)
                return CustomResponse<ClienteDto>.Fail("La identificación ya existe.");
            var ent = _mapper.Map<Cliente>(dto); await _repo.AgregarAsync(ent); dto.Id = ent.Id; return CustomResponse<ClienteDto>.Ok(dto);
        }
        public async Task<CustomResponse<ClienteDto>> ActualizarAsync(ClienteDto dto)
            => (await _repo.ActualizarAsync(_mapper.Map<Cliente>(dto))) ? CustomResponse<ClienteDto>.Ok(dto) : CustomResponse<ClienteDto>.Fail("No se pudo actualizar.");
        public Task<bool> EliminarAsync(int id) => _repo.EliminarAsync(id);
        public async Task<List<ClienteDto>> ListarAsync() => (await _repo.ObtenerTodosAsync()).Select(_mapper.Map<ClienteDto>).ToList();
    }

    public class SolicitudesServicio : ISolicitudesServicio
    {
        private readonly ISolicitudesRepositorio _repo;
        private readonly IClientesRepositorio _clientes;
        private readonly ITrackingsRepositorio _track;
        private readonly IMapper _mapper;
        private const decimal MONTO_MAX = 10_000_000M;

        public SolicitudesServicio(ISolicitudesRepositorio r, IClientesRepositorio c, ITrackingsRepositorio t, IMapper m)
        { _repo = r; _clientes = c; _track = t; _mapper = m; }

        public async Task<CustomResponse<SolicitudDto>> CrearAsync(int clienteId, string identificacion, decimal monto, string comentarios, string usuarioNombre, Rol rol)
        {
            if (rol != Rol.ServicioCliente && rol != Rol.Admin) return CustomResponse<SolicitudDto>.Fail("No autorizado.");
            if (monto > MONTO_MAX) return CustomResponse<SolicitudDto>.Fail("Monto supera 10,000,000 colones.");

            var todas = await _repo.ObtenerTodasAsync();
            var activa = todas.Any(s => s.IdentificacionCliente == identificacion &&
                (s.Estado == EstadoSolicitud.Registrado || s.Estado == EstadoSolicitud.Devolucion));
            if (activa) return CustomResponse<SolicitudDto>.Fail($"El usuario con identificación {identificacion} ya cuenta con una gestión activa.");

            var cli = await _clientes.ObtenerPorIdAsync(clienteId);
            if (cli is null) return CustomResponse<SolicitudDto>.Fail("Cliente no encontrado.");

            var ent = new SolicitudCredito { ClienteId = clienteId, IdentificacionCliente = identificacion, Monto = monto, Comentarios = comentarios, Estado = EstadoSolicitud.Registrado };
            await _repo.AgregarAsync(ent);
            await _track.AgregarAsync(new TrackingGestion { GestionId = ent.Id, Fecha = System.DateTime.Now, Accion = "Crear", Comentario = "Se crea la gestión para el cliente.", Usuario = $"{usuarioNombre}({rol})" });

            return CustomResponse<SolicitudDto>.Ok(_mapper.Map<SolicitudDto>(ent));
        }

        public async Task<CustomResponse<SolicitudDto>> EnviarAprobacionAsync(int id, string usuarioNombre, Rol rol, List<string>? nuevosDocs = null)
        {
            if (rol != Rol.Analista && rol != Rol.Admin) return CustomResponse<SolicitudDto>.Fail("No autorizado.");
            var s = await _repo.ObtenerPorIdAsync(id); if (s is null) return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");
            if (s.Estado is not (EstadoSolicitud.Ingresado or EstadoSolicitud.Devolucion or EstadoSolicitud.Registrado))
                return CustomResponse<SolicitudDto>.Fail("Estado inválido para enviar a aprobación.");

            if (nuevosDocs != null && nuevosDocs.Count > 0) s.Documentos.AddRange(nuevosDocs);
            s.Estado = EstadoSolicitud.EnviadoAprobacion; await _repo.ActualizarAsync(s);
            await _track.AgregarAsync(new TrackingGestion { GestionId = s.Id, Fecha = System.DateTime.Now, Accion = "Enviada aprobación", Comentario = "Se realiza el análisis y se envía aprobación", Usuario = $"{usuarioNombre}({rol})" });
            return CustomResponse<SolicitudDto>.Ok(_mapper.Map<SolicitudDto>(s));
        }

        public async Task<CustomResponse<SolicitudDto>> AprobarAsync(int id, string usuarioNombre, Rol rol)
        {
            if (rol != Rol.Gestor && rol != Rol.Admin) return CustomResponse<SolicitudDto>.Fail("No autorizado.");
            var s = await _repo.ObtenerPorIdAsync(id); if (s is null) return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");
            if (s.Estado != EstadoSolicitud.EnviadoAprobacion) return CustomResponse<SolicitudDto>.Fail("Solo gestiones enviadas a aprobación pueden aprobarse.");
            s.Estado = EstadoSolicitud.Aprobado; await _repo.ActualizarAsync(s);
            await _track.AgregarAsync(new TrackingGestion { GestionId = s.Id, Fecha = System.DateTime.Now, Accion = "Aprobada", Comentario = "Se acepta la gestión", Usuario = $"{usuarioNombre}({rol})" });
            return CustomResponse<SolicitudDto>.Ok(_mapper.Map<SolicitudDto>(s));
        }

        public async Task<CustomResponse<SolicitudDto>> DevolverAsync(int id, string comentario, string usuarioNombre, Rol rol)
        {
            if (rol != Rol.Gestor && rol != Rol.Admin) return CustomResponse<SolicitudDto>.Fail("No autorizado.");
            var s = await _repo.ObtenerPorIdAsync(id); if (s is null) return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");
            if (s.Estado != EstadoSolicitud.EnviadoAprobacion) return CustomResponse<SolicitudDto>.Fail("Solo gestiones enviadas a aprobación pueden devolverse.");
            s.Estado = EstadoSolicitud.Devolucion; await _repo.ActualizarAsync(s);
            await _track.AgregarAsync(new TrackingGestion { GestionId = s.Id, Fecha = System.DateTime.Now, Accion = "Devolución", Comentario = comentario, Usuario = $"{usuarioNombre}({rol})" });
            return CustomResponse<SolicitudDto>.Ok(_mapper.Map<SolicitudDto>(s));
        }

        public async Task<List<SolicitudDto>> ListarPorRolAsync(Rol rol)
        {
            var all = await _repo.ObtenerTodasAsync();
            var data = rol switch
            {
                Rol.Analista => all.Where(s => s.Estado == EstadoSolicitud.Ingresado || s.Estado == EstadoSolicitud.Devolucion || s.Estado == EstadoSolicitud.Registrado),
                Rol.Gestor => all.Where(s => s.Estado == EstadoSolicitud.EnviadoAprobacion),
                _ => all
            };
            return data.Select(_mapper.Map<SolicitudDto>).ToList();
        }

        public async Task<List<TrackingDto>> ObtenerTrackingAsync(int gestionId)
            => (await _track.ObtenerPorGestionAsync(gestionId))
               .Select(t => new TrackingDto { GestionId = t.GestionId, Fecha = t.Fecha, Accion = t.Accion, Comentario = t.Comentario, Usuario = t.Usuario }).ToList();
    }
}
