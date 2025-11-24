namespace SGC.DAL.Entidades
{
    public class SolicitudCredito
    {
        public int Id { get; set; }      // empieza en 11550 en BD
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string? Comentarios { get; set; }
        public string Estado { get; set; } = "Ingresado";
        public string? Documentos { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public Cliente Cliente { get; set; } = null!;
        public ICollection<TrackingGestion>? Trackings { get; set; }
    }
}
