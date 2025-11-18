using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(ApplicationDbContext context) : base(context) { }

        public async Task<Usuario> Login(string correo, string contrasena)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.Correo == correo && x.Contrasena == contrasena);
        }
    }
}
