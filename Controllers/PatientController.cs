﻿using MediSynthFinals.Data;
using MediSynthFinals.Models;
using MediSynthFinals.ViewModel;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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

        public IActionResult Profile()
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
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

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

            return NotFound();
        }

        // Add Diagnosis
        // TO DO: Transfer to doctor controller
        [HttpGet]
        public IActionResult Diagnosis()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Diagnosis(RecordDiagnosis edit)
        {
            var identityID = _userManager.GetUserId(User); // get user Id
            Console.WriteLine("USER ID" + identityID);

            PatientCredentials refNum = _dbContext.PatientCredentials.FirstOrDefault(r => r.patientRef == identityID);

            if (refNum != null)
            {
                // For DATABASE
                RecordDiagnosis patient = new RecordDiagnosis();
                patient.diagnosisText = edit.diagnosisText;
                patient.additionalNote = edit.additionalNote;
                patient.attendingDoctor = edit.attendingDoctor;
                patient.visitDate = edit.visitDate;
                patient.rtypeId = "Diagnosis";
                patient.patientId = identityID.ToString();

                _dbContext.RecordDiagnosis.Add(patient);
                _dbContext.SaveChanges();
                return RedirectToAction("Profile", "Patient");

            }

            return NotFound();
        }

    }
}
