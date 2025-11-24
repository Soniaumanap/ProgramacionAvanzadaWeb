namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        // Navegación
        public ICollection<SolicitudCredito> Solicitudes { get; set; } = new List<SolicitudCredito>();
    }
}
