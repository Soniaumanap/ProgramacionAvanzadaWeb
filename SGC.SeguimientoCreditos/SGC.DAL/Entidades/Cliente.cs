namespace SGC.DAL.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<SolicitudCredito>? Solicitudes { get; set; }
    }
}
