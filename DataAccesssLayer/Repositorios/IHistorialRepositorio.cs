using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface IHistorialRepositorio
    {
        Task<List<HistorialSolicitud>> ObtenerPorSolicitudAsync(int idSolicitud);
        Task<bool> RegistrarCambioAsync(HistorialSolicitud historial);
    }
}
