using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    public enum Gender
    {
        Female,
        Male
    }

    public enum Region
    {
        Ilocos,
        CagayanValley,
        CentralLuzon,
        CALABARZON,
        MIMAROPA,
        Bicol,
        WesternVisayas,
        CentralVisayas,
        EasternVisayas,
        ZamboangaPeninsula,
        NorthernMindanao,
        DavaoRegion,
        SOCCSKSARGEN,
        Caraga,
        NCR,
        CAR,
        BARMM
    }

    // TO ADD MORE CITIES
    public enum City
    {
        QuezonCity,
        Manila,
        Cavite,
        Pasig
    }

    [Table("credentials", Schema = "patient")]
    public class PatientCredentials
    {
        [Key]
        public int patientId { get; set; }
        public string patientRef { get; set; }

        [Display(Name = "First Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string fName { get; set; }

        [Display(Name = "Username")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string lName { get; set; }

        [Display(Name = "Street Address")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }

        [Display(Name = "Region")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string region { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string city { get; set; }

        [Display(Name = "Gender")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string gender { get; set; }

        [Display(Name = "Birthdate")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime birthdate { get; set; }

        [Display(Name = "Birthplace")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string birthplace { get; set; }

        [Display(Name = "Contact Number")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numeric values.")]
        public string contactNum { get; set; }

        [Display(Name = "Email Address")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string email { get; set; }

        [Display(Name = "Occupation")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string occupation { get; set; }

        [Display(Name = "Religion")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string religion { get; set; }

        [Display(Name = "Emergency Contact Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? emergencyName { get; set; }

        [Display(Name = "Emergency Contact Number")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numeric values.")]
        public string? emergencyNum { get; set; }
    }
}
