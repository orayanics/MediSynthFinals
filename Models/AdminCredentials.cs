using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("credentials", Schema = "admin")]
    public class AdminCredentials
    {
        [Key]
        public int admindId { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string contactNum { get; set; }
        public string email { get; set; }
    }
}
