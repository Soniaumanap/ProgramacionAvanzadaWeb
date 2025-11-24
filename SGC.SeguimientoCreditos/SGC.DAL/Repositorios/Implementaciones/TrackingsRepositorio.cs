using Microsoft.EntityFrameworkCore;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.DAL.Repositorios
{
    public class TrackingsRepositorio : ITrackingsRepositorio
    {
        private readonly SgcDbContext _context;

        public TrackingsRepositorio(SgcDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(TrackingGestion t)
        {
            _context.TrackingsGestion.Add(t); // TrackingsGestion (igual que en DbContext)
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TrackingGestion>> ListarPorGestionAsync(int gestionId)
        {
            return await _context.TrackingsGestion   
                .Where(x => x.GestionId == gestionId)
                .OrderBy(x => x.Fecha)
                .ToListAsync();
        }
    }
}
