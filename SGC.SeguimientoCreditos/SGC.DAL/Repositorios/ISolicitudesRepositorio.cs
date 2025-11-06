using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public interface ISolicitudesRepositorio
    {
        Task<List<SolicitudCredito>> ObtenerTodasAsync();
        Task<SolicitudCredito?> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(SolicitudCredito s);
        Task<bool> ActualizarAsync(SolicitudCredito s);
    }
}
