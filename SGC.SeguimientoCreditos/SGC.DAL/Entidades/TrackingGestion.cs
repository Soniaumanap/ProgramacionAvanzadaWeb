namespace SGC.DAL.Entidades
{
    public class TrackingGestion
    {
        public int Id { get; set; }
        public int GestionId { get; set; }
        public string Accion { get; set; } = null!;
        public string? Comentario { get; set; }
        public string UsuarioNombre { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.Now;

        public SolicitudCredito Solicitud { get; set; } = null!;
    }
}
