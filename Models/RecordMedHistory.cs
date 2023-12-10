using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("medhistory", Schema = "record")]
    public class RecordMedHistory
    {
        [Key]
        public int medhistoryId { get; set; }

        [Display(Name = "Past Medical History")]
        [Required(ErrorMessage = "Past Medical History is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastMedHistory { get; set; }

        [Display(Name = "Past Hospitalization")]
        [Required(ErrorMessage = "Past Hospitalizatin is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastHospitalization { get; set; }

        [Display(Name = "Past Surgical Operaiton")]
        [Required(ErrorMessage = "Past Surgical Operation is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastSurgicalOperation { get; set; }

        [Display(Name = "Medical Concern")]
        [Required(ErrorMessage = "Medical Concern is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string medConcern { get; set; }

        [Display(Name = "Food Allergies")]
        [Required(ErrorMessage = "Food Allergy is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string foodAllergy { get; set; }

        [Display(Name = "Drug Allergies")]
        [Required(ErrorMessage = "Drug Allergy is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string drugAllergy { get; set; }

        [Display(Name = "Attending Doctor")]
        [Required(ErrorMessage = "Attending Doctor is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string attendingDoctor { get; set; }

        [Display(Name = "Visit Date")]
        [Required(ErrorMessage = "Visit Date is required")]
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public string rtypeId { get; set; }
        public string patientId { get; set; }
    }
}
