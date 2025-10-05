using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProyectoUsuarios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> logger;
        private readonly IUsuariosServicio _usuariosServicio;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuariosServicio usuariosServicio)
        {
            this.logger = logger;
            _usuariosServicio = usuariosServicio;
        }

        public async Task<IActionResult> Index()
        {
            var respuesta = await _usuariosServicio.ObtenerUsuarioAsync();
            return View(respuesta.Data);
        }
    }
}
