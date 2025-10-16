using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProyectoUsuarios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuariosServicio _usuariosServicio;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuariosServicio usuariosServicio)
        {
            _logger = logger;
            _usuariosServicio = usuariosServicio;
        }

        public async Task<IActionResult> Index()
        {
            var respuesta = await _usuariosServicio.ObtenerUsuariosAsync();
            return View(respuesta.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                var respuesta = await _usuariosServicio.AgregarUsuarioAsync(usuarioDto);
                if (!respuesta.EsError)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            }
            return View(usuarioDto);
            } 
        }
    }

