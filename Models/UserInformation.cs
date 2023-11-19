using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediSynthFinals.Models
{
    [Table("information", Schema = "users")]
    public class UserInformation
    {
        // FOR DATABASE REFERENCE
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "FirstName")]
        public string? fName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "LastName")]
        public string? lName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public string? contactNum { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string? department { get; set; }
    }
}
