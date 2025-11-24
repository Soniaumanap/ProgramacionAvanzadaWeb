using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Implementaciones
{
    public class TrackingsRepositorio : ITrackingsRepositorio
    {
        private readonly SgcDbContext _context;

        public TrackingsRepositorio(SgcDbContext context)
        {
            _context = context;
        }

        public async Task AgregarAsync(TrackingGestion t)
        {
            _context.TrackingsGestion.Add(t);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TrackingGestion>> ObtenerPorGestionAsync(int gestionId)
        {
            return await _context.TrackingsGestion
                                 .AsNoTracking()
                                 .Where(x => x.GestionId == gestionId)
                                 .OrderBy(x => x.Fecha)
                                 .ToListAsync();
        }
    }
}
