using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace MediSynthFinals.Models
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

    public class UserCredentials : IdentityUser
    {
        // FOR TABLE 
        public string? fName { get; set; }
        public string? lName { get; set; }
        public string? department {  get; set; }
        public string? userRole { get; set; }
    }
}
