using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios
{
    public class TrackingGestionRepositorio : RepositorioGenerico<TrackingGestion>, ITrackingGestionRepositorio
    {
        public TrackingGestionRepositorio(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<TrackingGestion>> ObtenerPorSolicitud(int solicitudCreditoId)
        {
            return await _dbSet
                .Where(x => x.SolicitudCreditoId == solicitudCreditoId)
                .ToListAsync();
        }
    }
}
