using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface IUsuariosServicio
    {
        Task<List<UsuarioDto>> ObtenerTodosAsync();
        Task<UsuarioDto?> ObtenerPorIdAsync(int id);
        Task<UsuarioDto?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> CrearAsync(UsuarioDto dto);
        Task<bool> ActualizarAsync(UsuarioDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
