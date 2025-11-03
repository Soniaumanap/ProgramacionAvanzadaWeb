using System;

namespace SGC.DAL.Entidades
{
    public class TrackingGestion
    {
        public int GestionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Accion { get; set; } = "";
        public string Comentario { get; set; } = "";
        public string Usuario { get; set; } = ""; // Nombre (Rol)
    }
}
