using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("credentials", Schema ="doctor")]
    public class DoctorCredentials
    {
        [Key]
        public int doctorId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string contactNum { get; set; }
        public string department { get; set; }
        public string medLicense { get; set; }
    }
}
