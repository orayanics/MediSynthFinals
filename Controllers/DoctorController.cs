using MediSynthFinals.Data;
using MediSynthFinals.Models;
using MediSynthFinals.Utils;
using MediSynthFinals.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.Runtime.Versioning;
using System.Web.Helpers;

namespace MediSynthFinals.Controllers
{
    [Authorize(Roles = "DOCTOR, ADMIN")]
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

        public ActionResult Index()
        {
            var username = _userManager.GetUserName(User);
            List<UserInformation> doctor = _dbContext.UserInformation.Where(x => x.username == username).ToList();
            if (doctor != null)
            {
                foreach (var item in doctor)
                {
                    if (item.username == username)
                    {
                        var docId = item.userId;
                        var viewModel = new DoctorSchedViewModel
                        {
                            UserInformation = _dbContext.UserInformation.Where(x => x.username == username).ToList(),
                            UserSchedule = _dbContext.UserSchedules.Where(x => x.userId == docId)
                            .OrderBy(x => x.scheduleDate)
                            .ToList(),
                        };
                        return View(viewModel);
                    }
                }

            }
            return NotFound();

        }


        [HttpGet]
        public IActionResult Schedule()
        {
            //Search for the doctor whose id matches the given id
            var username = _userManager.GetUserName(User);
            List<UserInformation> doctors = _dbContext.UserInformation.Where(x => x.username == username).ToList();

            if (doctors != null)
            {
                foreach (var item in doctors)
                {
                    if (item.username == username)
                    {
                        var docId = item.userId;
                        UserSchedule? docsched = _dbContext.UserSchedules.FirstOrDefault(UserSchedule => UserSchedule.scheduleId == docId);
                        ViewBag.Id = docId;
                        return View(docsched);
                    }
                }


            }
            return NotFound();

        }

        [HttpPost]
        public IActionResult Schedule(UserSchedule input)
        {
            UserSchedule sched = new UserSchedule();
            sched.scheduleDate = input.scheduleDate;
            sched.scheduleInfo = input.scheduleInfo;
            sched.userId = input.userId;

            if (ModelState.IsValid)
            {
                _dbContext.UserSchedules.Add(sched);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Doctor");
            }
            return NotFound();

        }

        public IActionResult DeleteSched(int id)
        {
            UserSchedule sched = new UserSchedule();
            sched.scheduleId = id;

            if (sched != null)
            {
                _dbContext.UserSchedules.Remove(sched);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Doctor");
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Edit()
        {
            var username = _userManager.GetUserName(User);
            UserInformation info = _dbContext.UserInformation.FirstOrDefault(x => x.username == username);

            if (info != null)
            {
                var viewModel = new DoctorEditViewModel()
                {
                    userId = info.userId,
                    username = info.username,
                    fName = info.fName,
                    lName = info.lName,
                    email = info.email,
                    contactNum = info.contactNum,
                    department = info.department
                };
                return View(viewModel);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorEditViewModel user)
        {
            var username = _userManager.GetUserName(User);
            var email = user.email.ToUpper();
            UserInformation info = _dbContext.UserInformation.FirstOrDefault(x => x.userId == user.userId);

            if ((user.currentpass == null && user.newpass == null) || (user.currentpass ==null || user.newpass ==null))
            {
                return View(user);
            }

            if (info != null)
            {
                // For Database user.information
                info.fName = user.fName;
                info.lName = user.lName;
                info.email = user.email;
                info.contactNum = user.contactNum;
                info.department = user.department;
                _dbContext.UserInformation.Update(info);
                _dbContext.SaveChanges();

                // For Identity 
                var identity = await _userManager.FindByEmailAsync(email);
                identity.fName = user.fName;
                identity.lName = user.lName;
                identity.department = user.department;
                identity.Email = user.email;
                await _userManager.ChangePasswordAsync(identity, user.currentpass, user.newpass);

                return RedirectToAction("Profile", "Doctor");
            }
            return NotFound();

        }


        [HttpGet]
        public IActionResult Diagnosis(string id)
        {
            var patient = _dbContext.PatientCredentials.FirstOrDefault(p => p.patientRef == id);
            var date = DateTime.Now;
            if (patient != null)
            {
                // Create a view model for the diagnosis form
                var diagnosisViewModel = new DoctorDiagnosisViewModel
                {
                    patientId = patient.patientRef,
                    visitDate = date
                    // Add other properties as needed
                };

                return View(diagnosisViewModel);
            }
            return NotFound();

        }

        // FOR DIAGNOSIS SUBMISSION
        [HttpPost]
        public IActionResult Diagnosis(RecordDiagnosis form)
        {

            if (ModelState.IsValid)
            {
                Console.WriteLine("Patient ID: " + form.patientId);
               
                _dbContext.RecordDiagnosis.Add(form);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Doctor");
            }
            Console.WriteLine("NOTFOUND Patient ID: " + form.patientId);
            var user = new DoctorDiagnosisViewModel();
            return View(user);
        }

        // Add Medical History
        [HttpGet]
        public IActionResult AddHistory(string id)
        {
            var patient = _dbContext.PatientCredentials.FirstOrDefault(p => p.patientRef == id);
            var date = DateTime.Now;
            if (patient != null)
            {
                // Create a view model for the diagnosis form
                var diagnosisViewModel = new DoctorMedHisViewModel
                {
                    patientId = patient.patientRef,
                    visitDate = date
                    // Add other properties as needed
                };

                return View(diagnosisViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddHistory(RecordMedHistory edit)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Patient ID: " + edit.patientId);

                _dbContext.RecordMedHistory.Add(edit);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Doctor");
            }
            Console.WriteLine("NOTFOUND Patient ID: " + edit.patientId);

            return NotFound();
        }

        // PATIENT LIST
        [HttpGet]
        public ActionResult Patients()
        {
            List<PatientCredentials> patients = _dbContext.PatientCredentials.ToList();
            if (patients != null)
            {
                return View(patients);

            }
            return NotFound();

        }


        //// FOR SEARCH
        [HttpPost]
        public IActionResult Patients(string searchString)
        {
            List<PatientCredentials> users = _dbContext.PatientCredentials.Where(r => r.lName == searchString || r.fName == searchString || r.patientRef.Contains(searchString)).ToList();
            List<PatientCredentials> patients = _dbContext.PatientCredentials.ToList();

            if (users != null)
            {
                if (searchString.IsNullOrEmpty())
                {
                    return View(patients);
                }
                else {
                    return View(users);
                }
            }

            return NotFound();
        }

        // VIEW PATIENT DIAGNOSIS LIST
        [HttpGet]
        public ActionResult ViewDiagnosis()
        {
            List<RecordDiagnosis> patients = _dbContext.RecordDiagnosis.ToList();
            if (patients != null)
            {
                return View(patients);

            }
            return NotFound();

        }



    }
}

