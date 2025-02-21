using System.ComponentModel.DataAnnotations;

namespace Netolex.ViewModels.Account
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
