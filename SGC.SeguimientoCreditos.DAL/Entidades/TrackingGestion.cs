using System;

namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class TrackingGestion
    {
        public int TrackingGestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }

        public int SolicitudCreditoId { get; set; }
        public SolicitudCredito SolicitudCredito { get; set; }
    }
}
