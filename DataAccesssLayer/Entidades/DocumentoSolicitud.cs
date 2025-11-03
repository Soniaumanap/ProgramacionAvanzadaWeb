using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    /*
     Documento adjuntado a una solicitud de crédito.
     */
    public class DocumentoSolicitud
    {
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string UrlDocumento { get; set; }
        public DateTime FechaAdjunto { get; set; }

        //SolicitudCredito FK
        public int IdSolicitud { get; set; }
        public SolicitudCredito? Solicitud { get; set; }

        //Usuario FK: Quien adjuntó el documento
        public int IdUsuarioAdjunto { get; set; }
        public Usuario? UsuarioAdjunto { get; set; }
    }
}
