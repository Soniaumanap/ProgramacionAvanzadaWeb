using System;

namespace SGC.BLL.Dtos
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string? Comentarios { get; set; }
        public string Estado { get; set; } = null!;
        public string? Documentos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
