using MediSynthFinals.Data;
using MediSynthFinals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace MediSynthFinals.Controllers
{
    public class PatientController : Controller
    {
        // Db Context
        private readonly MediDbContext _dbContext;

        public PatientController(MediDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            return View(_dbContext.PatientCredentials);
        }

        public IActionResult Details(int id)
        {
            PatientCredentials? patient = _dbContext.PatientCredentials.FirstOrDefault(x => x.patientId == id);

            if (patient != null)
            {
                dynamic model = new ExpandoObject();
                model.PatientCredentials = _dbContext.PatientCredentials;
                model.RecordDiagnosis = _dbContext.RecordDiagnosis;
                model.RecordMedHistory = _dbContext.RecordMedHistory;
                {
                    if (model != null)
                    {        
                        return View(model);
                    }
                }
            }

            return NotFound();
        }

    }
}
