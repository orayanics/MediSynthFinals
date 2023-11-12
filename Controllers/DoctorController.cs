using MediSynthFinals.Data;
using MediSynthFinals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace MediSynthFinals.Controllers
{
    public class DoctorController : Controller
    {
        // Db Context
        private readonly MediDbContext _dbContext;

        public DoctorController(MediDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            return View(_dbContext.DoctorCredentials);
        }

        public IActionResult Details(int id) {
            DoctorCredentials? doc = _dbContext.DoctorCredentials.FirstOrDefault(x => x.doctorId == id);
            
            if (doc != null)
            {
                dynamic model = new ExpandoObject();
                model.DoctorCredentials = _dbContext.DoctorCredentials;
                model.DoctorSchedule = _dbContext.DoctorSchedules;
                {
                    if (model != null)
                    {
                        ViewBag.DoctorId = id;
                        return View(model);
                    }
                }
            }

            //return View(_dbContext.DoctorCredentials);
            return NotFound();
        }

    }
}
