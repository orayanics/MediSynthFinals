namespace MediSynthFinals.Models
{
    public class RecordMedHistory
    {
        public int medhistoryId { get; set; }
        public string pastMedHistory { get; set; }
        public string pastHospitalization { get; set; }
        public string pastSurgicalOperation { get; set; }
        public string medConcern { get; set; }
        public string foodAllergy { get; set; }
        public string drugAllergy { get; set; }
        public string attendingDoctor { get; set; }
        public DateOnly visitDate { get; set; }

        // FOREIGN KEYS
        public int rtypeId { get; set; }
        public int patientId { get; set; }
    }
}
