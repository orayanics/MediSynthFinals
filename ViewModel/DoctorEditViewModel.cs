using MediSynthFinals.Models;
using System.ComponentModel.DataAnnotations;

namespace MediSynthFinals.ViewModel
{
    public class DoctorEditViewModel
    {
        [Display(Name = "User ID")]
        public int userId { get; set; }

        [Display(Name = "Username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        [Display(Name = "Current Pass")]
        public string? currentpass { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "New Password")]
        public string? newpass { get; set; }

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
