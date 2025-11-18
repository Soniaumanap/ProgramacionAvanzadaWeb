using System;

namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class TrackingDto
    {
        public int TrackingGestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int SolicitudCreditoId { get; set; }
    }
}
