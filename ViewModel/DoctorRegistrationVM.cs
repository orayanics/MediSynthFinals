using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediSynthFinals.ViewModel
{
    public class DoctorRegistrationVM
    {
        public enum Department
        {
            Anesthesiology,
            Cardiology,
            Dermatology,
            [Description("General Surgery")] GS,
            Geriatrics,
            Gynaecology,
            Hematology,
            [Description("Internel Medicine")] IM,
            [Description("Intensive Care Medicine")] ICM,
            Medicine,
            Nephrology,
            Neurology,
            Obstetrics,
            Ophthalmology,
            Orthopedics,
            Otohinolaryngology,
            Pathology,
            Pediatrics,
            [Description("Plastic Surgery")] PS,
            Radiology,
            Rheumatology,
            Surgery,
            Urology
        }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string? password { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "FirstName")]
        public string? fName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "LastName")]
        public string? lName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public string? contactNum { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string? department { get; set; }
    }
}
