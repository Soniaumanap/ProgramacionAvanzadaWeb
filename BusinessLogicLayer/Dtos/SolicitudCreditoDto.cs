using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class SolicitudCreditoDto
    {
        public int IdSolicitud { get; set; }
        public decimal MontoCredito { get; set; }
        public string comentarios { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdEstado { get; set; }
        public EstadoSolicitud? Estado { get; set; }
        public int UsuarioCreacionId { get; set; }
        public Usuario? UsuarioCreacion { get; set; }
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
