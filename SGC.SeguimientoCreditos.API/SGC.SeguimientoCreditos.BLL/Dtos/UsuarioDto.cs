using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}
