using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class HistorialSolicitudDto
    {
        public int IdHistorial { get; set; }
        public int IdSolicitud { get; set; }
        public SolicitudCredito? Solicitud { get; set; }
        public int IdEstadoAnterior { get; set; }
        public EstadoSolicitud? EstadoAnterior { get; set; }
        public int IdEstadoNuevo { get; set; }
        public EstadoSolicitud? EstadoNuevo { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaCambio { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
