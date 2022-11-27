using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RegService.ViewModel
{
    public class RegisterViewModel
    {
        //public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "Email Not Matched")]
        public string ConfirmEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
    }
}
