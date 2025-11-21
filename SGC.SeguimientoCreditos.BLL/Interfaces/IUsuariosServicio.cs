using SGC.SeguimientoCreditos.BLL.Dtos;

namespace SGC.SeguimientoCreditos.BLL.Interfaces
{
    public interface IUsuariosServicio
    {
        Task<UsuarioDto> Login(string correo, string contrasena);
        Task<List<UsuarioDto>> ObtenerTodosAsync();
    }
}
