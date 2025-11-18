namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }

        public ICollection<SolicitudCredito> SolicitudesCredito { get; set; }
    }
}
