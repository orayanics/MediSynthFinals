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

    }


}
