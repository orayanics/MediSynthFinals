using Microsoft.AspNetCore.Mvc;

namespace MediSynthFinals.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
