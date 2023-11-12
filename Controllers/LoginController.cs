using Microsoft.AspNetCore.Mvc;

namespace MediSynthFinals.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
