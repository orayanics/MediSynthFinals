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
    [Authorize(Roles = "DOCTOR, ADMIN")]
    public class AdminController : Controller
    {
        private MediDbContext _dbContext;
        private readonly SignInManager<UserCredentials> _signInManager;
        private readonly UserManager<UserCredentials> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
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
        public IActionResult Doctors()
        {
            var doctors = _dbContext.UserInformation.ToList().Where(x => x.department != "PATIENT");
            return View(doctors);
        }

        // REMOVE DOCTOR CONFIRMATION
        [HttpGet]
        public IActionResult ConfirmDoc(string? email)
        {
            var doctors = _dbContext.UserInformation.ToList().FirstOrDefault(x => x.email == email);
            return View(doctors);
        }
 
        // REMOVE DOCTOR
        [HttpPost]
        public async Task<IActionResult> RemoveDoctor(string email)
        {
            email = email.ToUpper();
            Console.WriteLine("EMAIL: " + email);
            UserInformation doctor = _dbContext.UserInformation.FirstOrDefault(x => x.email == email);

            if (doctor != null)
            {
                // Remove the doctor from the UserInformation 
                _dbContext.UserInformation.Remove(doctor);
                _dbContext.SaveChanges();

                // Find the user by email using UserManager
                var find = await _userManager.FindByEmailAsync(email.ToUpper());

                if (find != null)
                {
                    // Delete the user using UserManager
                    await _userManager.DeleteAsync(find);

                    // Remove the "DOCTOR" role from the user
                    await _userManager.RemoveFromRoleAsync(find, "DOCTOR");

                    // Redirect to the "Doctors" action in the "Admin" controller
                    return RedirectToAction("Doctors", "Admin");
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        // Register Patient
        [HttpGet]
        public IActionResult Patients()
        {
            var patients = _dbContext.PatientCredentials.ToList();
            return View(patients);
        }

        // REMOVE PATIENT CONFIRMATION
        [HttpGet]
        public IActionResult ConfirmPatient(int id)
        {
            var patient = _dbContext.UserInformation.ToList().FirstOrDefault(x => x.userId == id);
            return View(patient);
        }

        // REMOVE PATENT
        [HttpPost]
        public async Task<IActionResult> RemovePatient(int id, string refId)
        {
            refId = refId.ToUpper();
            Console.WriteLine("REF ID: " + refId);
            UserInformation patient = _dbContext.UserInformation.FirstOrDefault(x => x.userId == id);
            PatientCredentials pa = _dbContext.PatientCredentials.FirstOrDefault(x => x.patientId == id);
            if (patient != null)
            {
                _dbContext.PatientCredentials.Remove(pa);
                _dbContext.UserInformation.Remove(patient);
                _dbContext.SaveChanges();

                var find = await _userManager.FindByEmailAsync(refId);

                if (find != null)
                {
                    await _userManager.DeleteAsync(find);
                    await _userManager.RemoveFromRoleAsync(find, "PATIENT");
                    return RedirectToAction("Patients", "Admin");
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

    }
}
