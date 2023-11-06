namespace MediSynthFinals.Models
{
    public class RecordDiagnosis
    {
        public int diagnosisId { get; set; }
        public string diagnosisText { get; set; }
        public string additionalNote { get; set; }
        public string attendingDoctor { get; set; }
        public DateOnly visitDate { get; set; }

        // FOREIGN KEYS
        public int rtypeId { get; set; }
        public int patientId { get; set; }
    }
}
