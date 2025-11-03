using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int idCliente);
        Task<Cliente> ObtenerPorIdentificacionAsync(int identificacion);
        Task<bool> CrearAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int idCliente);
    }
}
