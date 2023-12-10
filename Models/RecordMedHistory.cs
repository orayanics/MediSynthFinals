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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastMedHistory { get; set; }

        [Display(Name = "Past Hospitalization")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastHospitalization { get; set; }

        [Display(Name = "Past Surgical Operaiton")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string pastSurgicalOperation { get; set; }

        [Display(Name = "Medical Concern")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string medConcern { get; set; }

        [Display(Name = "Food Allergies")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string foodAllergy { get; set; }

        [Display(Name = "Drug Allergies")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string drugAllergy { get; set; }

        [Display(Name = "Attending Doctor")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string attendingDoctor { get; set; }

        [Display(Name = "Visit Date")]
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public string rtypeId { get; set; }
        public string patientId { get; set; }
    }
}
