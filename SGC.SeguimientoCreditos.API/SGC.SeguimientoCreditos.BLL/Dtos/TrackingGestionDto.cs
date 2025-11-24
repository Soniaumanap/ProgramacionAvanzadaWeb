using System;

namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class TrackingGestionDto
    {
        public int Id { get; set; }
        public int GestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }
        public int UsuarioId { get; set; }
    }
}
