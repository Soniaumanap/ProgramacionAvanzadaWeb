using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    /*
     Catálogo de estados por los que pasa una solicitud de crédito.
     */
    public class EstadoSolicitud
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public string Descripcion { get; set; }
    }
}
