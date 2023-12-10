using MediSynthFinals.Models;

namespace MediSynthFinals.ViewModel
{
    public class DoctorDiagnosisViewModel
    {
        public int diagnosisId { get; set; }
        public string diagnosisText { get; set; }
        public string additionalNote { get; set; }
        public string attendingDoctor { get; set; }
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public string rtypeId { get; set; }
        public string patientId { get; set; }
    }
}
