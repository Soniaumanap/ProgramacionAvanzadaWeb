using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    /*
     Cliente solicitante del crédito.
     */
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int Identificacion { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public bool Activo { get; set; }
    }
}
