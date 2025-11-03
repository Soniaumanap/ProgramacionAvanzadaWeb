using Microsoft.AspNetCore.Mvc;
using SGC.BLL.Dtos;
using SGC.BLL.Servicios;

namespace SGC.Web.Controllers
{
    // Login ===
    public class AuthController : Controller
    {
        private readonly IUsuariosServicio _usuarios;
        public AuthController(IUsuariosServicio usuarios) { _usuarios = usuarios; }

        [HttpGet] public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var u = await _usuarios.BuscarPorEmailPassAsync(dto.Email, dto.Password);
            if (u == null) { ViewBag.Error = "Credenciales inválidas"; return View(); }
            HttpContext.Session.SetString("Nombre", u.Nombre);
            HttpContext.Session.SetString("Rol", u.Rol.ToString());
            return RedirectToAction("Index", "Home");
        }

        // Registro ===
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var resp = await _usuarios.RegistrarAsync(dto);
            if (resp.EsError)
            {
                ViewBag.Error = resp.Mensaje;
                return View(dto);
            }

            TempData["Registered"] = "Cuenta creada. Inicie sesión con sus credenciales.";
            return RedirectToAction("Login");

        }


        public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Login"); }
    }
}
