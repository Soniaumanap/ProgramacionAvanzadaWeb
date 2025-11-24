namespace SGC.DAL.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!; // Admin, Analista, Gestor, ServicioCliente
        public bool Activo { get; set; } = true;

        public ICollection<TrackingGestion>? Trackings { get; set; }
    }
}
