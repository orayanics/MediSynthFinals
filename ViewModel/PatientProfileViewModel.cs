using MediSynthFinals.Models;

namespace MediSynthFinals.ViewModel
{
    public class PatientProfileViewModel
    {
        public List<PatientCredentials> PatientCredentials { get; set; }
        public List<RecordDiagnosis> RecordDiagnosis { get; set; }
        public List<RecordMedHistory> RecordMedHistory { get; set; }
    }
}
