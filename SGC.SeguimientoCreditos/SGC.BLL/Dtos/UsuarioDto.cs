using SGC.DAL.Entidades;

namespace SGC.BLL.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public Rol Rol { get; set; }
        public bool Activo { get; set; } = true;
    }
}
