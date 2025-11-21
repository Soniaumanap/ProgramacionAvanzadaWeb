using System;

namespace SGC.SeguimientoCreditos.DAL.Entidades
{
    public class SolicitudCredito
    {
        public int SolicitudCreditoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<TrackingGestion> TrackingsGestion { get; set; }
    }
}
