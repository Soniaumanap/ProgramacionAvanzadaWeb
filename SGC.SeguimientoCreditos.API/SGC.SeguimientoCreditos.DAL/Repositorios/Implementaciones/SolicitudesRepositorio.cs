using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Implementaciones
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
            var existente = await _context.SolicitudesCredito.FindAsync(s.Id);
            if (existente is null) return false;

            existente.ClienteId = s.ClienteId;
            existente.IdentificacionCliente = s.IdentificacionCliente;
            existente.Monto = s.Monto;
            existente.Comentarios = s.Comentarios;
            existente.Estado = s.Estado;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<SolicitudCredito?> ObtenerPorIdAsync(int id)
        {
            return await _context.SolicitudesCredito
                                 .AsNoTracking()
                                 .Include(s => s.Cliente)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SolicitudCredito>> ObtenerTodasAsync()
        {
            return await _context.SolicitudesCredito
                                 .AsNoTracking()
                                 .Include(s => s.Cliente)
                                 .ToListAsync();
        }
    }
}
