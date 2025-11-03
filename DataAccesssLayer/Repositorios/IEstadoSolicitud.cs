using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface IEstadoSolicitud
    {
        Task<List<EstadoSolicitud>> ObtenerTodosAsync();
        Task<EstadoSolicitud?> ObtenerPorIdAsync(int idEstado);
    }
}
