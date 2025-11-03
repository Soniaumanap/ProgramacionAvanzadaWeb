using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public interface ITrackingsRepositorio
    {
        Task<List<TrackingGestion>> ObtenerPorGestionAsync(int gestionId);
        Task AgregarAsync(TrackingGestion t);
    }
}
