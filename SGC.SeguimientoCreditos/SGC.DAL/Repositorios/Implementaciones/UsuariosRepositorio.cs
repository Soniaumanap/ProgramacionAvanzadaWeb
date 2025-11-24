using Microsoft.EntityFrameworkCore;
using SGC.DAL.Entidades;
using SGC.DAL.Repositorios.Interfaces;

namespace SGC.DAL.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly SgcDbContext _context;

        public UsuariosRepositorio(SgcDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(Usuario u)
        {
            _context.Usuarios.Add(u);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(Usuario u)
        {
            _context.Usuarios.Update(u);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return false;

            _context.Usuarios.Remove(u);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> ObtenerAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario?> ObtenerPorIdentificacionAsync(string id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Identificacion == id);
        }

        public async Task<List<Usuario>> ListarAsync()
        {
            return await _context.Usuarios
                .OrderBy(x => x.Nombre)
                .ToListAsync();
        }
    }
}
