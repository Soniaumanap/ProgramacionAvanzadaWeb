using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private const string ROL_ADMIN = "Admin";
        private const string ROL_ANALISTA = "Analista";
        private const string ROL_GESTOR = "Gestor";
        private const string ROL_SERVICIO = "ServicioCliente";

        private const string ESTADO_INGRESADO = "Ingresado";
        private const string ESTADO_DEVOLUCION = "Devolucion";
        private const string ESTADO_ENVIADO_APROBACION = "Enviado aprobacion";
        private const string ESTADO_APROBADO = "Aprobado";

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

        public async Task<List<SolicitudDto>> ListarPorRolAsync(string rol)
        {
            var lista = await _repo.ListarAsync();

            IEnumerable<SolicitudCredito> filtrada = lista;

            if (rol == ROL_ANALISTA)
            {
                filtrada = lista.Where(x =>
                    x.Estado == ESTADO_INGRESADO ||
                    x.Estado == ESTADO_DEVOLUCION);
            }
            else if (rol == ROL_GESTOR)
            {
                filtrada = lista.Where(x =>
                    x.Estado == ESTADO_ENVIADO_APROBACION);
            }
            else if (rol == ROL_ADMIN)
            {
                filtrada = lista; // todas
            }
            else
            {
                filtrada = Enumerable.Empty<SolicitudCredito>();
            }

            return _mapper.Map<List<SolicitudDto>>(filtrada.ToList());
        }

        public async Task<CustomResponse<SolicitudDto>> CrearAsync(
            int clienteId,
            string identificacionCliente,
            decimal monto,
            string? comentarios,
            string usuarioNombre,
            string rol)
        {
            if (rol != ROL_SERVICIO && rol != ROL_ADMIN)
            {
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para crear solicitudes.");
            }

            if (monto > MONTO_MAXIMO)
            {
                return CustomResponse<SolicitudDto>.Fail("No puede ingresar un monto mayor a 10.000.000 colones.");
            }

            var cliente = await _clientes.ObtenerPorIdentificacionAsync(identificacionCliente);
            if (cliente == null)
            {
                return CustomResponse<SolicitudDto>.Fail("El cliente no está registrado.");
            }

            var solicitudExistente = await _repo.ObtenerPorClienteActivo(identificacionCliente);
            if (solicitudExistente != null &&
                (solicitudExistente.Estado == ESTADO_INGRESADO ||
                 solicitudExistente.Estado == ESTADO_DEVOLUCION))
            {
                string msg = $"El usuario con identificación {identificacionCliente} ya cuenta con la solicitud de crédito {solicitudExistente.Id}, " +
                             "por favor resolver la gestión antes de ingresar otra nueva";
                return CustomResponse<SolicitudDto>.Fail(msg);
            }

            var ent = new SolicitudCredito
            {
                ClienteId = cliente.Id,
                IdentificacionCliente = identificacionCliente,
                Monto = monto,
                Comentarios = comentarios,
                Estado = ESTADO_INGRESADO,
                Documentos = null,
                Fecha = DateTime.Now
            };

            var ok = await _repo.AgregarAsync(ent);
            if (!ok)
            {
                return CustomResponse<SolicitudDto>.Fail("No se pudo crear la solicitud de crédito.");
            }

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = ent.Id,
                Accion = "Crear",
                Comentario = comentarios ?? "Se crea la gestión",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(ent);
            return CustomResponse<SolicitudDto>.Success(dto, "Solicitud creada correctamente.");
        }

        public async Task<CustomResponse<SolicitudDto>> EnviarAprobacionAsync(
            int gestionId,
            string usuarioNombre,
            string rol)
        {
            if (rol != ROL_ANALISTA && rol != ROL_ADMIN)
            {
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para enviar a aprobación.");
            }

            var s = await _repo.ObtenerAsync(gestionId);
            if (s == null)
            {
                return CustomResponse<SolicitudDto>.Fail("Solicitud no encontrada.");
            }

            if (s.Estado != ESTADO_INGRESADO && s.Estado != ESTADO_DEVOLUCION)
            {
                return CustomResponse<SolicitudDto>.Fail("La solicitud no está en un estado válido para enviar a aprobación.");
            }

            s.Estado = ESTADO_ENVIADO_APROBACION;

            var ok = await _repo.ActualizarAsync(s);
            if (!ok)
            {
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la solicitud.");
            }

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = s.Id,
                Accion = "Enviada aprobación",
                Comentario = "Se envía a aprobación",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(s);
            return CustomResponse<SolicitudDto>.Success(dto, "Solicitud enviada a aprobación.");
        }

        public async Task<CustomResponse<SolicitudDto>> AprobarAsync(
            int gestionId,
            string usuarioNombre,
            string rol)
        {
            if (rol != ROL_GESTOR && rol != ROL_ADMIN)
            {
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para aprobar.");
            }

            var s = await _repo.ObtenerAsync(gestionId);
            if (s == null)
            {
                return CustomResponse<SolicitudDto>.Fail("Solicitud no encontrada.");
            }

            if (s.Estado != ESTADO_ENVIADO_APROBACION)
            {
                return CustomResponse<SolicitudDto>.Fail("La solicitud no está en estado 'Enviado aprobación'.");
            }

            s.Estado = ESTADO_APROBADO;

            var ok = await _repo.ActualizarAsync(s);
            if (!ok)
            {
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la solicitud.");
            }

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = s.Id,
                Accion = "Aprobada",
                Comentario = "Se acepta la gestión",
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(s);
            return CustomResponse<SolicitudDto>.Success(dto, "Solicitud aprobada.");
        }

        public async Task<CustomResponse<SolicitudDto>> DevolverAsync(
            int gestionId,
            string comentario,
            string usuarioNombre,
            string rol)
        {
            if (rol != ROL_GESTOR && rol != ROL_ADMIN)
            {
                return CustomResponse<SolicitudDto>.Fail("No tiene permisos para devolver.");
            }

            var s = await _repo.ObtenerAsync(gestionId);
            if (s == null)
            {
                return CustomResponse<SolicitudDto>.Fail("Solicitud no encontrada.");
            }

            if (s.Estado != ESTADO_ENVIADO_APROBACION)
            {
                return CustomResponse<SolicitudDto>.Fail("La solicitud no está en estado 'Enviado aprobación'.");
            }

            s.Estado = ESTADO_DEVOLUCION;

            var ok = await _repo.ActualizarAsync(s);
            if (!ok)
            {
                return CustomResponse<SolicitudDto>.Fail("No se pudo actualizar la solicitud.");
            }

            await _track.AgregarAsync(new TrackingGestion
            {
                GestionId = s.Id,
                Accion = "Devolución",
                Comentario = comentario,
                UsuarioNombre = usuarioNombre,
                Fecha = DateTime.Now
            });

            var dto = _mapper.Map<SolicitudDto>(s);
            return CustomResponse<SolicitudDto>.Success(dto, "Solicitud devuelta.");
        }

        public async Task<List<TrackingDto>> ObtenerTrackingAsync(int gestionId)
        {
            var lista = await _track.ListarPorGestionAsync(gestionId);
            return _mapper.Map<List<TrackingDto>>(lista);
        }
    }
}
