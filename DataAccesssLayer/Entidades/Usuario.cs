using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesssLayer.Entidades
{
    /*
    Usuario del sistema con credenciales y rol.
     */
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string passwordHash { get; set; }
        public bool activo { get; set; }

        // Rol FK: Rol del usuario en el sistema
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }
    }
}