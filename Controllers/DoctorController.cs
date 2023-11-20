using MediSynthFinals.Data;
using MediSynthFinals.Models;
using MediSynthFinals.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace MediSynthFinals.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DoctorController : Controller
    {
        // Db Context
        private readonly MediDbContext _dbContext;
        private readonly UserManager<UserCredentials> _userManager;

        public DoctorController(
            MediDbContext dbContext,
            UserManager<UserCredentials> userManager
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var doctors = _dbContext.UserInformation.ToList();
            return View(doctors);
        }

        //public IActionResult Details(int id)
        //{
        //    UserCredentials doc = _dbContext.UserCredentials.FirstOrDefault(x => x.userId == id);

        //    if (doc != null)
        //    {
        //        dynamic model = new ExpandoObject();
        //        model.UserCredentials = _dbContext.UserCredentials;
        //        model.DoctorSchedule = _dbContext.DoctorSchedules;
        //        {
        //            if (model != null)
        //            {
        //                ViewBag.DoctorId = id;
        //                return View(model);
        //            }
        //        }
        //    }

        //    //return View(_dbContext.DoctorCredentials);
        //    return NotFound();
        //}

    }
}
