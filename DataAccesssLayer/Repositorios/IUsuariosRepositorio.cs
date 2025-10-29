using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<Usuario> ObtenerPorIdAsync(int idUsuario);
        Task<Usuario> ObtenerPorEmailAsync(string email);
        Task<bool> CrearAsync(Usuario usuario);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAsync(int idUsuario);
        Task<Usuario?> LoginAsync(string email, string passwordHash);
    }
}
