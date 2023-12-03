using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediSynthFinals.Models;
using MediSynthFinals.ViewModel;
using MediSynthFinals.Data;
using System.Drawing;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace MediSynthFinals.Controllers
{
    public class UserController : Controller
    {
        private MediDbContext _dbContext;
        private readonly SignInManager<UserCredentials> _signInManager;
        private readonly UserManager<UserCredentials> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(
            MediDbContext dbContext,
            SignInManager<UserCredentials> signInManager,
            UserManager<UserCredentials> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel logInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(logInfo.Username, logInfo.password, logInfo.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Failed to login");
            }
            return View(logInfo);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Doctor()
        {
            return View();
        }

        // Register Doctor
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Doctor(RegisterViewModel userEnteredData, UserInformation userInfo)
        {
            if (ModelState.IsValid)
            {
                // For DATABASE
                _dbContext.UserInformation.Add(userInfo);

                // Identity User
                UserCredentials user = new UserCredentials();
                user.UserName = userEnteredData.username;
                user.fName = userEnteredData.fName;
                user.lName = userEnteredData.lName;
                user.Email = userEnteredData.email;
                user.PhoneNumber = userEnteredData.contactNum;
                user.department = userEnteredData.department;
                user.userRole = "DOCTOR";

                var result = await _userManager.CreateAsync(user, userEnteredData.password);

                if (result.Succeeded)
                {
                    var defaultrole = _roleManager.FindByNameAsync("DOCTOR").Result;

                    if (defaultrole != null)
                    {
                        Microsoft.AspNetCore.Identity.IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(userEnteredData);
        }

        // Register Patient
        [HttpGet]
        public IActionResult Patient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Patient(RegisterViewModel userEnteredData, UserInformation userInfo)
        {
            if (ModelState.IsValid)
            {

                // Identity User
                UserCredentials user = new UserCredentials();
                user.UserName = userEnteredData.username;
                user.fName = userEnteredData.fName;
                user.lName = userEnteredData.lName;
                user.Email = userEnteredData.email;
                user.PhoneNumber = userEnteredData.contactNum;
                user.department = userEnteredData.department;
                user.userRole = "PATIENT";

                // For DATABASE
                _dbContext.UserInformation.Add(userInfo);

                PatientCredentials patient = new PatientCredentials();

                patient.patientRef = user.Id;
                patient.fName = userEnteredData.fName;
                patient.lName = userEnteredData.lName;
                patient.contactNum = userEnteredData.contactNum;
                patient.address = "";
                patient.region = "";
                patient.city = "";
                patient.gender = "";
                patient.birthdate = DateTime.Now;
                patient.email = userEnteredData.email;
                patient.birthplace = "";
                patient.occupation = "";
                patient.religion = "";
                patient.emergencyName = "";
                patient.emergencyNum = "";

                _dbContext.PatientCredentials.Add(patient);

                var result = await _userManager.CreateAsync(user, userEnteredData.password);

                if (result.Succeeded)
                {
                    var defaultrole = _roleManager.FindByNameAsync("PATIENT").Result;

                    if (defaultrole != null)
                    {
                        Microsoft.AspNetCore.Identity.IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(userEnteredData);
        }

        // Logout User
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
