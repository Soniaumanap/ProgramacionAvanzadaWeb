using Microsoft.EntityFrameworkCore;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.DAL.Repositorios
{
    public class SolicitudesRepositorio : ISolicitudesRepositorio
    {
        private readonly SgcDbContext _context;

        public SolicitudesRepositorio(SgcDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(SolicitudCredito s)
        {
            _context.SolicitudesCredito.Add(s);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(SolicitudCredito s)
        {
            _context.SolicitudesCredito.Update(s);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<SolicitudCredito?> ObtenerAsync(int id)
        {
            return await _context.SolicitudesCredito
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<SolicitudCredito>> ListarAsync()
        {
            return await _context.SolicitudesCredito
                .Include(x => x.Cliente)
                .OrderByDescending(x => x.Fecha)
                .ToListAsync();
        }

        // Devuelve la última solicitud registrada para esa identificación
        public async Task<SolicitudCredito?> ObtenerPorClienteActivo(string identificacion)
        {
            return await _context.SolicitudesCredito
                .Where(x => x.IdentificacionCliente == identificacion)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var sol = await _context.SolicitudesCredito.FindAsync(id);
            if (sol == null) return false;

            _context.SolicitudesCredito.Remove(sol);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
