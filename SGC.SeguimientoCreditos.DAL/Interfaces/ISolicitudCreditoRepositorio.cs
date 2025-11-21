using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Interfaces
{
    public interface ISolicitudCreditoRepositorio : IRepositorioGenerico<SolicitudCredito>
    {
        Task<IEnumerable<SolicitudCredito>> ObtenerPorCliente(int clienteId);
    }
}
