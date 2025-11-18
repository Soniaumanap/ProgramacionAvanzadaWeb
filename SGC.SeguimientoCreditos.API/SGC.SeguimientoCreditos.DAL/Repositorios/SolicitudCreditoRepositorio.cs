using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios
{
    public class SolicitudCreditoRepositorio : RepositorioGenerico<SolicitudCredito>, ISolicitudCreditoRepositorio
    {
        public SolicitudCreditoRepositorio(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<SolicitudCredito>> ObtenerPorCliente(int clienteId)
        {
            return await _dbSet
                .Where(x => x.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
