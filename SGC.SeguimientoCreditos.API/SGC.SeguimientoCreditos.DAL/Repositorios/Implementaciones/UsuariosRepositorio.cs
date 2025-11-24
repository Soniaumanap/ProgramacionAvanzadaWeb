using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios.Implementaciones
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
            var existente = await _context.Usuarios.FindAsync(u.Id);
            if (existente is null) return false;

            existente.Identificacion = u.Identificacion;
            existente.Nombre = u.Nombre;
            existente.Email = u.Email;
            existente.Password = u.Password;
            existente.Rol = u.Rol;
            existente.Activo = u.Activo;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente is null) return false;

            _context.Usuarios.Remove(existente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _context.Usuarios
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Identificacion == identificacion);
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await _context.Usuarios
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
