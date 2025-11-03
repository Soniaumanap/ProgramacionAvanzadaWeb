using System;

namespace SGC.BLL.Dtos
{
    public class TrackingDto
    {
        public int GestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Accion { get; set; } = "";
        public string Comentario { get; set; } = "";
        public string Usuario { get; set; } = "";
    }
}
