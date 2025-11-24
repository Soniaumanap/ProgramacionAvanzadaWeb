namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class SolicitudCredito
    {
        public int Id { get; set; }                // Identity (11550,1)
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string? Comentarios { get; set; }
        public string? Estado { get; set; }

        // Navegación
        public Cliente? Cliente { get; set; }
    }
}
