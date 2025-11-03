using Microsoft.AspNetCore.Mvc;
using SGC.Web.Utils;

namespace SGC.Web.Controllers
{
    [RoleAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Nombre = HttpContext.Session.GetString("Nombre");
            ViewBag.Rol = HttpContext.Session.GetString("Rol");
            return View();
        }
    }
}
