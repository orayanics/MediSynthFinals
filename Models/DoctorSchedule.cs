using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("schedule", Schema = "doctor")]
    public class DoctorSchedule
    {
        [Key]
        public int scheduleId { get; set; }
        public string? scheduleDate { get; set; }
        public string? scheduleInfo { get; set; }

        // Foreign Key
        public int doctorId { get; set; }
    }
}
