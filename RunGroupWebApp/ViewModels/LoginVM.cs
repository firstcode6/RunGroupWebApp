using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
