using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("credentials", Schema = "users")]
    public class UserCredentials
    {
        [Key]
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string contactNum { get; set; }
        public string department {  get; set; }
        public string userRole { get; set; }
    }
}
