using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly List<Usuario> _usuarios = new()
        {
            new Usuario{ Id=1, Identificacion="1-111-111", Nombre="Admin", Email="admin@sgc.com", Password="admin", Rol=Rol.Admin },
            new Usuario{ Id=2, Identificacion="2-222-222", Nombre="Ana Analista", Email="ana@sgc.com", Password="123", Rol=Rol.Analista },
            new Usuario{ Id=3, Identificacion="3-333-333", Nombre="Gus Gestor", Email="gus@sgc.com", Password="123", Rol=Rol.Gestor },
            new Usuario{ Id=4, Identificacion="4-444-444", Nombre="Sofi SC", Email="sc@sgc.com", Password="123", Rol=Rol.ServicioCliente },
        };

        public Task<bool> AgregarAsync(Usuario u) { u.Id = _usuarios.Any() ? _usuarios.Max(x => x.Id) + 1 : 1; _usuarios.Add(u); return Task.FromResult(true); }
        public Task<bool> ActualizarAsync(Usuario u)
        {
            var e = _usuarios.FirstOrDefault(x => x.Id == u.Id); if (e is null) return Task.FromResult(false);
            e.Identificacion = u.Identificacion; e.Nombre = u.Nombre; e.Email = u.Email; e.Password = u.Password; e.Rol = u.Rol; e.Activo = u.Activo;
            return Task.FromResult(true);
        }
        public Task<bool> EliminarAsync(int id) { var e = _usuarios.FirstOrDefault(x => x.Id == id); if (e is null) return Task.FromResult(false); _usuarios.Remove(e); return Task.FromResult(true); }
        public Task<Usuario?> ObtenerPorIdAsync(int id) => Task.FromResult(_usuarios.FirstOrDefault(x => x.Id == id));
        public Task<Usuario?> ObtenerPorIdentificacionAsync(string identificacion) => Task.FromResult(_usuarios.FirstOrDefault(x => x.Identificacion == identificacion));
        public Task<List<Usuario>> ObtenerUsuariosAsync() => Task.FromResult(_usuarios.ToList());
    }
}
