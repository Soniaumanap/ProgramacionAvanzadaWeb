using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Servicios;
using SGC.BLL.Dtos;

namespace SGC.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuariosServicio _usuarios;

        public AuthController(IUsuariosServicio usuarios)
        {
            _usuarios = usuarios;
        }

        // LOGIN
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _usuarios.LoginAsync(email, password);
            if (user == null)
            {
                TempData["Error"] = "Credenciales incorrectas";
                return View();
            }

            // Guardar sesión
            HttpContext.Session.SetString("UsuarioId", user.Id.ToString());
            HttpContext.Session.SetString("UsuarioNombre", user.Nombre);
            HttpContext.Session.SetString("UsuarioRol", user.Rol);

            return RedirectToAction("Index", "Home");
        }

        // REGISTRO
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioDto dto)
        {
            var resp = await _usuarios.RegistrarAsync(dto);

            if (!resp.Ok)
            {
                TempData["Error"] = resp.Mensaje;
                return View();
            }

            TempData["Success"] = "Usuario registrado correctamente";
            return RedirectToAction("Login");
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
