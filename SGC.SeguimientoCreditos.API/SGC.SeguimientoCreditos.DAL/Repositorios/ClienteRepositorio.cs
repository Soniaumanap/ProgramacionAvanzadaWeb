using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Entidades;
using SGC.SeguimientoCreditos.DAL.Interfaces;

namespace SGC.SeguimientoCreditos.DAL.Repositorios
{
    public class ClienteRepositorio : RepositorioGenerico<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ApplicationDbContext context) : base(context) { }
    }
}
