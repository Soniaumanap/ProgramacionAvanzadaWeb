using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGC.DAL.Entidades;

namespace SGC.DAL.Repositorios
{
    public class TrackingsRepositorio : ITrackingsRepositorio
    {
        private readonly List<TrackingGestion> _items = new();
        public Task AgregarAsync(TrackingGestion t) { _items.Add(t); return Task.CompletedTask; }
        public Task<List<TrackingGestion>> ObtenerPorGestionAsync(int id)
            => Task.FromResult(_items.Where(x => x.GestionId == id).OrderBy(x => x.Fecha).ToList());
    }
}
