namespace MediSynthFinals.Models
{
    public class DoctorSchedule
    {
        public int scheduleId { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleInfo { get; set; }

        // Foreign Key
        public int doctorId { get; set; }
    }
}
