using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public class SolicitudesRepositorio : ISolicitudesRepositorio
    {
        private readonly List<SolicitudCredito> _sol = new();
        public Task<bool> AgregarAsync(SolicitudCredito s) { s.Id = _sol.Any() ? _sol.Max(x => x.Id) + 1 : 11550; _sol.Add(s); return Task.FromResult(true); }
        public Task<bool> ActualizarAsync(SolicitudCredito s)
        {
            var e = _sol.FirstOrDefault(x => x.Id == s.Id); if (e is null) return Task.FromResult(false);
            e.ClienteId = s.ClienteId; e.IdentificacionCliente = s.IdentificacionCliente; e.Monto = s.Monto; e.Comentarios = s.Comentarios; e.Estado = s.Estado; e.Documentos = s.Documentos; return Task.FromResult(true);
        }
        public Task<SolicitudCredito?> ObtenerPorIdAsync(int id) => Task.FromResult(_sol.FirstOrDefault(x => x.Id == id));
        public Task<List<SolicitudCredito>> ObtenerTodasAsync() => Task.FromResult(_sol.ToList());
    }
}
