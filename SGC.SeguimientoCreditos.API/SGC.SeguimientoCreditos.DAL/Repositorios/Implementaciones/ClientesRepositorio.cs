using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Implementaciones
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly SgcDbContext _context;

        public ClientesRepositorio(SgcDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(Cliente c)
        {
            _context.Clientes.Add(c);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(Cliente c)
        {
            var existente = await _context.Clientes.FindAsync(c.Id);
            if (existente is null) return false;

            existente.Identificacion = c.Identificacion;
            existente.Nombre = c.Nombre;
            existente.Telefono = c.Telefono;
            existente.Email = c.Email;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existente = await _context.Clientes.FindAsync(id);
            if (existente is null) return false;

            _context.Clientes.Remove(existente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _context.Clientes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }
    }
}
