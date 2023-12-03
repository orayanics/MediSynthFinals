using MediSynthFinals.Data;
using MediSynthFinals.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace MediSynthFinals.Controllers
{
    public class PatientController : Controller
    {
        // Db Context
        private readonly MediDbContext _dbContext;
        private readonly UserManager<UserCredentials> _userManager;

        public PatientController(MediDbContext dbContext, UserManager<UserCredentials> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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

        public IActionResult Profile()
        {
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

            PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);

            if(refNum != null)
            {
                return View(refNum);

            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

            PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);

            if (refNum != null)
            {
                return View(refNum);

            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(PatientCredentials edit)
        {
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

            PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);
            
            if (refNum != null)
            {
                refNum.fName = edit.fName;
                refNum.lName = edit.lName;
                refNum.contactNum = edit.contactNum;
                refNum.address = edit.address;
                refNum.region = edit.region;
                refNum.city = edit.city;
                refNum.gender = edit.gender;
                refNum.birthdate = edit.birthdate;
                refNum.birthplace = edit.birthplace;
                refNum.occupation = edit.occupation;
                refNum.religion = edit.religion;
                refNum.emergencyName = edit.emergencyName;
                refNum.emergencyNum =  edit.emergencyNum;
                _dbContext.PatientCredentials.Update(refNum);
                _dbContext.SaveChanges();
                return View(refNum);
            }

            return NotFound();
        }

    }
}
