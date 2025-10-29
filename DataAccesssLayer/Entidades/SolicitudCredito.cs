using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    /*
     Solicitud de crédito: núcleo del flujo SGC.
     */
    public class SolicitudCredito
    {
        public int IdSolicitud { get; set; }
        public decimal MontoCredito { get; set; }
        public string comentarios { get; set; }
        public DateTime FechaSolicitud { get; set; }

        //Estado FK: Estado actual de la solicitud de crédito
        public int IdEstado { get; set; }
        public EstadoSolicitud? Estado { get; set; }

        //UsuarioCreacion FK: Usuario que creó la solicitud de crédito
        public int UsuarioCreacionId { get; set; }
        public Usuario? UsuarioCreacion { get; set; }

        //Cliente FK: Cliente de la solicitud de crédito
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }


    }
}
