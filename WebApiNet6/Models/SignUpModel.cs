using System.ComponentModel.DataAnnotations;

namespace WebApiNet6.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
