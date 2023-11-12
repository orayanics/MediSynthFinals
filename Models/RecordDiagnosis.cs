using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("diagnosis", Schema = "record")]
    public class RecordDiagnosis
    {
        [Key]
        public int diagnosisId { get; set; }
        public string diagnosisText { get; set; }
        public string additionalNote { get; set; }
        public string attendingDoctor { get; set; }
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public int rtypeId { get; set; }
        public int patientId { get; set; }
    }
}
