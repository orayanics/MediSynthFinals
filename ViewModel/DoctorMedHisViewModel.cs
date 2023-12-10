using MediSynthFinals.Models;
using System.ComponentModel.DataAnnotations;

namespace MediSynthFinals.ViewModel
{
    public class DoctorMedHisViewModel
    {
        public int medhistoryId { get; set; }

        [Display(Name = "Past Medical History")]
        public string pastMedHistory { get; set; }

        [Display(Name = "Past Hospitalization")]
        public string pastHospitalization { get; set; }

        [Display(Name = "Past Surgical Operaiton")]
        public string pastSurgicalOperation { get; set; }

        [Display(Name = "Medical Concern")]
        public string medConcern { get; set; }

        [Display(Name = "Food Allergies")]
        public string foodAllergy { get; set; }
        [Display(Name = "Drug Allergies")]
        public string drugAllergy { get; set; }

        [Display(Name = "Attending Doctor")]
        public string attendingDoctor { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public string rtypeId { get; set; }
        public string patientId { get; set; }
    }
}
