//using MediSynthFinals.Data;
//using MediSynthFinals.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;

//namespace MediSynthFinals.Controllers
//{
//    public class LoginController : Controller
//    {
//        // Db Context
//        private readonly MediDbContext _dbContext;

//        public LoginController(MediDbContext dbContext)
//        {
//            _dbContext = dbContext;

//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(UserCredentials user)
//        {
//            var username = user.username;
//            var password = user.password;
//            if (user != null)
//            {
//                var findUser = _dbContext.UserCredentials.Where(u => u.username == username && u.password == password).FirstOrDefault();
//                if (findUser != null)
//                {
//                    Console.WriteLine("user:" + user.username + ":" + user.password);
//                    Console.WriteLine("user found");
//                    return RedirectToAction("Index", "Doctor");
//                }
//            }
//            Console.WriteLine(username + ":" +password);
//            Console.WriteLine("user not found");
//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
