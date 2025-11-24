using Microsoft.AspNetCore.Mvc;

namespace SGC.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            var usuarioRol = HttpContext.Session.GetString("UsuarioRol");

            if (string.IsNullOrEmpty(usuarioNombre))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Nombre = usuarioNombre;
            ViewBag.Rol = usuarioRol;

            return View();
        }
    }
}
