namespace SGC.DAL.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = ""; // version_1: plano (luego se hasheamos)
        public Rol Rol { get; set; }
        public bool Activo { get; set; } = true;
    }
}
