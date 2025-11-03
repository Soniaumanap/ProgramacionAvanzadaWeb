using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface ISolicitudCreditoRepositorio
    {
        Task<List<SolicitudCredito>> ObtenerTodasAsync();
        Task<SolicitudCredito?> ObtenerPorIdAsync(int idSolicitud);
        Task<bool> CrearAsync(SolicitudCredito solicitud);
        Task<bool> ActualizarAsync(SolicitudCredito solicitud);
        Task<bool> CambiarEstadoAsync(int idSolicitud, int nuevoEstadoId, string comentario, int idUsuario);
        Task<bool> EliminarAsync(int idSolicitud);
    }
}
