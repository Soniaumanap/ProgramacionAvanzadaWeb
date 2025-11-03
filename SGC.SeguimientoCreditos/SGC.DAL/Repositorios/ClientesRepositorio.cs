using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly List<Cliente> _clientes = new()
        {
            new Cliente{ Id=1, Identificacion="10101010", Nombre="Juan Perez", Telefono="8888-8888", Email="juan@x.com"},
            new Cliente{ Id=2, Identificacion="20202020", Nombre="Maria Gomez", Telefono="7777-7777", Email="maria@x.com"}
        };

        public Task<bool> AgregarAsync(Cliente c) { c.Id = _clientes.Any() ? _clientes.Max(x => x.Id) + 1 : 1; _clientes.Add(c); return Task.FromResult(true); }
        public Task<bool> ActualizarAsync(Cliente c)
        {
            var e = _clientes.FirstOrDefault(x => x.Id == c.Id); if (e is null) return Task.FromResult(false);
            e.Identificacion = c.Identificacion; e.Nombre = c.Nombre; e.Telefono = c.Telefono; e.Email = c.Email; return Task.FromResult(true);
        }
        public Task<bool> EliminarAsync(int id) { var e = _clientes.FirstOrDefault(x => x.Id == id); if (e is null) return Task.FromResult(false); _clientes.Remove(e); return Task.FromResult(true); }
        public Task<List<Cliente>> ObtenerTodosAsync() => Task.FromResult(_clientes.ToList());
        public Task<Cliente?> ObtenerPorIdAsync(int id) => Task.FromResult(_clientes.FirstOrDefault(x => x.Id == id));
        public Task<Cliente?> ObtenerPorIdentificacionAsync(string id) => Task.FromResult(_clientes.FirstOrDefault(x => x.Identificacion == id));
    }
}
