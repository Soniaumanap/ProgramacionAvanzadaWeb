using System;

namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class SolicitudDto
    {
        public int SolicitudCreditoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int ClienteId { get; set; }
    }
}
