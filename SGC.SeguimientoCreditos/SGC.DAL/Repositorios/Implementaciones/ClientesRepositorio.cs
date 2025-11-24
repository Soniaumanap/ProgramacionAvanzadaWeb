using Microsoft.EntityFrameworkCore;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.DAL.Repositorios
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
            _context.Clientes.Update(c);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var c = await _context.Clientes.FindAsync(id);
            if (c == null) return false;

            _context.Clientes.Remove(c);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Cliente?> ObtenerAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(x => x.Identificacion == identificacion);
        }

        public async Task<List<Cliente>> ListarAsync()
        {
            return await _context.Clientes
                .OrderBy(x => x.Nombre)
                .ToListAsync();
        }
    }
}
