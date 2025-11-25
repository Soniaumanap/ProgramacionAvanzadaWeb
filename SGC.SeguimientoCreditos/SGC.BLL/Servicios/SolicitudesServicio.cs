using AutoMapper;
using SGC.BLL.Dtos;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.BLL.Servicios
{
    public class SolicitudesServicio : ISolicitudesServicio
    {
        private readonly ISolicitudesRepositorio _repo;
        private readonly IClientesRepositorio _clientes;
        private readonly ITrackingsRepositorio _track;
        private readonly IMapper _mapper;

        private const decimal MONTO_MAXIMO = 10000000m;

        public SolicitudesServicio(
            ISolicitudesRepositorio repo,
            IClientesRepositorio clientes,
            ITrackingsRepositorio track,
            IMapper mapper)
        {
            _repo = repo;
            _clientes = clientes;
            _track = track;
            _mapper = mapper;
        }

        // ===================== LISTAR POR ROL =====================
        public async Task<List<SolicitudDto>> ListarPorRolAsync(string rol)
        {
            var lista = await _repo.ListarAsync();

            IEnumerable<SolicitudCredito> filtrada = lista;

            switch (rol)
            {
                case "Analista":
                    filtrada = lista.Where(s =>
                        s.Estado == "Ingresado" ||
                        s.Estado == "Devolucion" ||
                        s.Estado == "Devolución");
                    break;

                case "Gestor":
                    filtrada = lista.Where(s =>
                        s.Estado == "Enviado aprobacion" ||
                        s.Estado == "Enviado aprobación");
                    break;

                case "Admin":
                default:
                    // Admin ve todas
                    break;
            }

            filtrada = filtrada.OrderByDescending(s => s.Fecha);

            return _mapper.Map<List<SolicitudDto>>(filtrada.ToList());
        }

        // ===================== CREAR =====================
        public async Task<CustomResponse<SolicitudDto>> CrearAsync(
            int clienteId,
            string identificacionCliente,
            decimal monto,
            string comentarios,
            string usuarioNombre,
            string rol)
        {
            if (monto > MONTO_MAXIMO)
                return CustomResponse<SolicitudDto>.Fail("No se permiten solicitudes por un monto mayor a 10.000.000 colones.");

            // Validar que el cliente exista (por si llaman directo desde API)
            var cliente = await _clientes.ObtenerAsync(clienteId);
            if (cliente == null)
                return CustomResponse<SolicitudDto>.Fail("El cliente no existe.");

            // Validar que no exista otra solicitud activa (Ingresado o Devolución)
            var existentes = await _repo.ListarAsync();
            var conActiva = existentes.FirstOrDefault(s =>
                s.IdentificacionCliente == identificacionCliente &&
                (s.Estado == "Ingresado" || s.Estado == "Devolucion" || s.Estado == "Devolución"));

            if (conActiva != null)
            {
                return CustomResponse<SolicitudDto>.Fail(
                    $"El usuario con identificación {identificacionCliente} ya cuenta con la solicitud de crédito {conActiva.Id}, " +
                    "por favor resolver la gestión antes de ingresar otra nueva."
                );
            }

            var nueva = new SolicitudCredito
            {
                ClienteId = clienteId,
                IdentificacionCliente = identificacionCliente,
                Monto = monto,
                Comentarios = comentarios,
                Estado = "Ingresado",
                Fecha = DateTime.Now
            };

            var ok = await _repo.AgregarAsync(nueva);
            if (!ok)
                return CustomResponse<SolicitudDto>.Fail("No se pudo crear la solicitud.");

            // Tracking: Crear
            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = nueva.Id,
                Accion = "Crear",
                Comentario = $"Se crea la gestión para el cliente {cliente.Nombre}.",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(nueva);
            return CustomResponse<SolicitudDto>.Success(dto, "Solicitud creada correctamente.");
        }

        // ===================== ENVIAR A APROBACIÓN =====================
        public async Task<CustomResponse<SolicitudDto>> EnviarAprobacionAsync(
            int gestionId, string usuarioNombre, string rol)
        {
            if (rol != "Analista" && rol != "Admin")
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para enviar solicitudes a aprobación.");

            var sol = await _repo.ObtenerAsync(gestionId);
            if (sol == null)
                return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");

            if (sol.Estado != "Ingresado" &&
                sol.Estado != "Devolucion" &&
                sol.Estado != "Devolución")
            {
                return CustomResponse<SolicitudDto>.Fail("Solo se pueden enviar a aprobación gestiones en estado Ingresado o Devolución.");
            }

            sol.Estado = "Enviado aprobación";

            if (!await _repo.ActualizarAsync(sol))
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la gestión.");

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = sol.Id,
                Accion = "Enviada aprobación",
                Comentario = "Se realiza el análisis y se envía a aprobación.",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(sol);
            return CustomResponse<SolicitudDto>.Success(dto, "La gestión fue enviada a aprobación correctamente.");
        }

        // ===================== APROBAR =====================
        public async Task<CustomResponse<SolicitudDto>> AprobarAsync(
            int gestionId, string usuarioNombre, string rol)
        {
            if (rol != "Gestor" && rol != "Admin")
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para aprobar gestiones.");

            var sol = await _repo.ObtenerAsync(gestionId);
            if (sol == null)
                return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");

            if (sol.Estado != "Enviado aprobacion" &&
                sol.Estado != "Enviado aprobación")
            {
                return CustomResponse<SolicitudDto>.Fail("Solo se pueden aprobar gestiones en estado Enviado aprobación.");
            }

            sol.Estado = "Aprobado";

            if (!await _repo.ActualizarAsync(sol))
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la gestión.");

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = sol.Id,
                Accion = "Aprobada",
                Comentario = "Se acepta la gestión.",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(sol);
            return CustomResponse<SolicitudDto>.Success(dto, "La gestión fue aprobada correctamente.");
        }

        // ===================== DEVOLVER =====================
        public async Task<CustomResponse<SolicitudDto>> DevolverAsync(
            int gestionId, string comentario, string usuarioNombre, string rol)
        {
            if (rol != "Gestor" && rol != "Admin")
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para devolver gestiones.");

            var sol = await _repo.ObtenerAsync(gestionId);
            if (sol == null)
                return CustomResponse<SolicitudDto>.Fail("Gestión no encontrada.");

            if (sol.Estado != "Enviado aprobacion" &&
                sol.Estado != "Enviado aprobación")
            {
                return CustomResponse<SolicitudDto>.Fail("Solo se pueden devolver gestiones en estado Enviado aprobación.");
            }

            sol.Estado = "Devolucion";

            if (!await _repo.ActualizarAsync(sol))
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la gestión.");

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = sol.Id,
                Accion = "Devolución",
                Comentario = comentario,
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(sol);
            return CustomResponse<SolicitudDto>.Success(dto, "La gestión fue devuelta para reproceso.");
        }

        // ===================== TRACKING =====================
        public async Task<List<TrackingDto>> ObtenerTrackingAsync(int gestionId)
        {
            var lista = await _track.ListarPorGestionAsync(gestionId);
            return _mapper.Map<List<TrackingDto>>(lista);
        }

        // ===================== Eliminar Gestion =====================
        public async Task<CustomResponse<bool>> EliminarAsync(int id, string usuarioNombre, string rol)
        {
            if (rol != "ServicioCliente" && rol != "Analista" && rol != "Admin" && rol != "Gestor")
                return CustomResponse<bool>.Fail("No tiene permisos para eliminar gestiones.");

            var gestion = await _repo.ObtenerAsync(id);
            if (gestion == null)
                return CustomResponse<bool>.Fail("La gestión no existe.");

            // Solo se permite eliminar gestiones NO aprobadas
            if (gestion.Estado == "Aprobado")
                return CustomResponse<bool>.Fail("No se puede eliminar una gestión aprobada.");

            var ok = await _repo.EliminarAsync(id);
            if (!ok)
                return CustomResponse<bool>.Fail("No se pudo eliminar la gestión.");

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = id,
                Accion = "Eliminar",
                Comentario = "Gestión eliminada",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            return CustomResponse<bool>.Success(true, "Gestión eliminada correctamente.");
        }

    }
}
