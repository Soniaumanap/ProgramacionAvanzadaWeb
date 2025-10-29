using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class DocumentoSolicitudDto
    {
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string UrlDocumento { get; set; }
        public DateTime FechaAdjunto { get; set; }
        public int IdSolicitud { get; set; }
        public SolicitudCredito? Solicitud { get; set; }
        public int IdUsuarioAdjunto { get; set; }
        public Usuario? UsuarioAdjunto { get; set; }
    }
}
