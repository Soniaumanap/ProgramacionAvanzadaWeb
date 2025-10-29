using DataAccesssLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Repositorios
{
    public interface IDocumentoSolicitudRepositorio
    {
        Task<List<DocumentoSolicitud>> ObtenerPorSolicitudAsync(int idSolicitud);
        Task<bool> AgregarAsync(DocumentoSolicitud documento);
        Task<bool> EliminarAsync(int idDocumento);
    }
}
