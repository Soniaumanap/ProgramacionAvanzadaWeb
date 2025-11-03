using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    public class HistorialSolicitud
    {
        public int IdHistorial { get; set; }

        //SolicitudCredito FK
        public int IdSolicitud { get; set; }
        public SolicitudCredito? Solicitud { get; set; }

        //Transición de estado
        public int IdEstadoAnterior { get; set; }
        public EstadoSolicitud? EstadoAnterior { get; set; }
        public int IdEstadoNuevo { get; set; }
        public EstadoSolicitud? EstadoNuevo { get; set; }

        public string Comentarios { get; set; }
        public DateTime FechaCambio { get; set; }

        //Usuario FK: Quién realizó el cambio
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
