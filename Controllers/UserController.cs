using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediSynthFinals.Models;
using MediSynthFinals.ViewModel;

namespace MediSynthFinals.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<UserCredentials> _signInManager;
        private readonly UserManager<UserCredentials> _userManager;

        public UserController(SignInManager<UserCredentials> signInManager, UserManager<UserCredentials> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        // Admin
        [HttpPost]
        public async Task<IActionResult> Admin(AdminViewModel userEnteredData)
        {
            if (ModelState.IsValid)
            {
                UserCredentials user = new UserCredentials();
                user.UserName = userEnteredData.username;
                user.fName = userEnteredData.fName;
                user.lName = userEnteredData.lName;
                user.Email = userEnteredData.email;
                user.PhoneNumber = userEnteredData.contactNum;
                user.department = userEnteredData.department;
                user.userRole = "ADMIN";

                var result = await _userManager.CreateAsync(user, userEnteredData.password);

                if (result.Succeeded)
                {
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

        [HttpGet]
        public IActionResult Doctor()
        {
            return View();
        }

        // Doctor
        [HttpPost]
        public async Task<IActionResult> Doctor(DoctorRegistrationVM userEnteredData)
        {
            if (ModelState.IsValid)
            {
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
