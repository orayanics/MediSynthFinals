using MediSynthFinals.Data;
using MediSynthFinals.Models;
using MediSynthFinals.Utils;
using MediSynthFinals.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

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

        public IActionResult Index()
        {
            var doctors = _dbContext.UserInformation.ToList();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult Profile()
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
                            UserSchedule = _dbContext.UserSchedules.Where(x => x.userId == docId).ToList(),
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
                    if(item.username == username)
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

            if (sched != null)
            {
                _dbContext.UserSchedules.Add(sched);
                _dbContext.SaveChanges();
                return RedirectToAction("Profile", "Doctor");
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
                return RedirectToAction("Profile", "Doctor");
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

        //[HttpPost]
        //public IActionResult Edit(UserInformation ChangeDocCredentials)
        //{
        //    UserCredentials? doctor = _dbContext.UserInformation.FirstOrDefault(UserCredentials => UserCredentials.userId == ChangeDocCredentials.userId);

        //    UserCredentials.FirstOrDefault(UserCredentials => UserCredentials.userId == ChangeDocCredentials.userId);
        //    if (doctor != null)
        //    {
        //        doctor.userId = ChangeDocCredentials.userId;
        //        doctor.username = ChangeDocCredentials.username;
        //        doctor.password = ChangeDocCredentials.password;
        //        doctor.fName = ChangeDocCredentials.fName;
        //        doctor.lName = ChangeDocCredentials.lName;
        //        doctor.email = ChangeDocCredentials.email;
        //        doctor.contactNum = ChangeDocCredentials.contactNum;
        //        doctor.department = ChangeDocCredentials.department;
        //        doctor.userRole = ChangeDocCredentials.userRole;

        //    }

        //    return View("Index", _dbContext.UserCredentials);
        //}

    }


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
