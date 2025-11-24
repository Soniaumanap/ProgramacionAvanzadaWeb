using System;

namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class TrackingGestion
    {
        public int Id { get; set; }
        public int GestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
