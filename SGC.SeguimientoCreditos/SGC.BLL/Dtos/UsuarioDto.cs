namespace SGC.BLL.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public bool Activo { get; set; } = true;
    }
}
