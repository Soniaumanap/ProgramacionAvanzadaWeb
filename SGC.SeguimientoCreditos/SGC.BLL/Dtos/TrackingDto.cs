using System;

namespace SGC.BLL.Dtos
{
    public class TrackingDto
    {
        public int Id { get; set; }
        public int GestionId { get; set; }
        public string Accion { get; set; } = null!;
        public string? Comentario { get; set; }
        public string UsuarioNombre { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
