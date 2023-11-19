using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediSynthFinals.Models;
using MediSynthFinals.ViewModel;
using Microsoft.AspNetCore.Authentication;


namespace MediSynthFinals.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<UserCredentials> _signInManager;
        private readonly UserManager<UserCredentials> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(
            SignInManager<UserCredentials> signInManager,
            UserManager<UserCredentials> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
                    var defaultrole = _roleManager.FindByNameAsync("ADMIN").Result;

                    if (defaultrole != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
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
                await _userManager.AddToRoleAsync(user, "USER");
                user.userRole = "DOCTOR";

                var result = await _userManager.CreateAsync(user, userEnteredData.password);

                if (result.Succeeded)
                {
                    var defaultrole = _roleManager.FindByNameAsync("USER").Result;

                    if (defaultrole != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
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
