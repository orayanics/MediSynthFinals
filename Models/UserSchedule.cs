using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("schedule", Schema = "users")]
    public class UserSchedule
    {
        [Key]
        public int scheduleId { get; set; }
        public string? scheduleDate { get; set; }
        public string? scheduleInfo { get; set; }

        // Foreign Key
        public int userId { get; set; }
    }
}
