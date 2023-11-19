//using MediSynthFinals.Data;
//using MediSynthFinals.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Dynamic;

//namespace MediSynthFinals.Controllers
//{
//    public class DoctorController : Controller
//    {
//        // Db Context
//        private readonly MediDbContext _dbContext;

//        public DoctorController(MediDbContext dbContext)
//        {
//            _dbContext = dbContext;

//        }

//        public IActionResult Index()
//        {
//            return View(_dbContext.UserCredentials.Where(x => x.userRole == "Staff"));
//        }

//        public IActionResult Details(int id) {
//            UserCredentials doc = _dbContext.UserCredentials.FirstOrDefault(x => x.userId == id);
            
//            if (doc != null)
//            {
//                dynamic model = new ExpandoObject();
//                model.UserCredentials = _dbContext.UserCredentials;
//                model.DoctorSchedule = _dbContext.DoctorSchedules;
//                {
//                    if (model != null)
//                    {
//                        ViewBag.DoctorId = id;
//                        return View(model);
//                    }
//                }
//            }

//            //return View(_dbContext.DoctorCredentials);
//            return NotFound();
//        }

//    }
//}
