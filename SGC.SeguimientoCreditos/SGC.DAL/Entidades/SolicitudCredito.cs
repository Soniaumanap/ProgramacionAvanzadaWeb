using System;
using System.Collections.Generic;

namespace SGC.DAL.Entidades
{
    public class SolicitudCredito
    {
        public int Id { get; set; }                        // Nº Gestión
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = "";
        public decimal Monto { get; set; }
        public string Comentarios { get; set; } = "";
        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Registrado;
        public List<string> Documentos { get; set; } = new();
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
