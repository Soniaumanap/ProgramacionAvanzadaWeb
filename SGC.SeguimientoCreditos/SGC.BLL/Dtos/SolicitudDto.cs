using System;
using System.Collections.Generic;
using SGC.DAL.Entidades;

namespace SGC.BLL.Dtos
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = "";
        public decimal Monto { get; set; }
        public string Comentarios { get; set; } = "";
        public EstadoSolicitud Estado { get; set; }
        public List<string> Documentos { get; set; } = new();
        public DateTime FechaCreacion { get; set; }
    }
}
