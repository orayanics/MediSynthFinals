using MediSynthFinals.Data;
using MediSynthFinals.Models;
using MediSynthFinals.ViewModel;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace MediSynthFinals.Controllers
{
    [Authorize(Roles = "PATIENT")]
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

        public IActionResult Doctors()
        {
            return View(_dbContext.UserInformation.Where(x => x.department != "PATIENT"));
        }

        [HttpGet]
        public IActionResult Schedule(int id)
        {
            var viewModel = new DoctorSchedViewModel
            {
                UserInformation = _dbContext.UserInformation.Where(x => x.userId == id)
                .OrderBy(x => x.department)
                .ToList(),
                UserSchedule = _dbContext.UserSchedules.Where(x => x.userId == id)
                .OrderBy(x => x.scheduleDate)
                .ToList(),
            };

            if (viewModel != null)
            {
                return View(viewModel);

            }

            return NotFound();
        }

        public IActionResult Details(int id)
        {
            PatientCredentials? patient = _dbContext.PatientCredentials.FirstOrDefault(x => x.patientId == id);

            if (patient != null)
            {
                dynamic model = new ExpandoObject();
                model.RecordDiagnosis = _dbContext.RecordDiagnosis;
                model.PatientCredentials = _dbContext.PatientCredentials;
                model.RecordMedHistory = _dbContext.RecordMedHistory;

                {
                    if (model.PatientCredentials != null && model.RecordMedHistory != null && model.RecordDiagnosis != null)
                    {        
                        return View(model);
                    }
                }
            }

            return NotFound();
        }

        public IActionResult Index()
        {
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

            PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);

            if (refNum != null)
            {
                var viewModel = new PatientProfileViewModel
                {
                    PatientCredentials = _dbContext.PatientCredentials.ToList(),
                    RecordMedHistory = _dbContext.RecordMedHistory.ToList(),
                    RecordDiagnosis = _dbContext.RecordDiagnosis.ToList()
                };
                return View(viewModel);

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

        // Add Medical History
        [HttpGet]
        public IActionResult AddHistory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHistory(RecordMedHistory edit)
        {

            if(ModelState.IsValid)
            {
                var identityID = _userManager.GetUserId(User); // get user Id

                PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);
                if (refNum != null)
                {
                    // For DATABASE
                    RecordMedHistory patient = new RecordMedHistory();
                    patient.pastHospitalization = edit.pastHospitalization;
                    patient.pastMedHistory = edit.pastMedHistory;
                    patient.pastSurgicalOperation = edit.pastSurgicalOperation;
                    patient.medConcern = edit.medConcern;
                    patient.drugAllergy = edit.drugAllergy;
                    patient.foodAllergy = edit.foodAllergy;
                    patient.attendingDoctor = edit.attendingDoctor;
                    patient.visitDate = edit.visitDate;
                    patient.rtypeId = "MedicalHistory";
                    patient.patientId = identityID.ToString();

                    _dbContext.RecordMedHistory.Add(patient);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Profile", "Patient");
                }
            }
            return View(edit);
        }

    }
}
