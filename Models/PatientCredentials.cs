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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string fName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string lName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string region { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string city { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string gender { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime birthdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string birthplace { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string contactNum { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string occupation { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string religion { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? emergencyName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? emergencyNum { get; set; }
    }
}
