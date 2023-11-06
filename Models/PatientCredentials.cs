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

    public class PatientCredentials
    {
        public int patientId { get; set; }
        public string patientRef { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string address { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string gender { get; set; }
        public DateOnly birthdate { get; set; }
        public string birthplace { get; set; }
        public string contactNum { get; set; }
        public string occupation { get; set; }
        public string religion { get; set; }
        public string? rmergencyName { get; set; }
        public int? emergencyNum { get; set; }
    }
}
