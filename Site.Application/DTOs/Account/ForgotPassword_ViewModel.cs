using System.ComponentModel.DataAnnotations;

namespace Site.Application.DTOs.Account
{
    public class ForgotPassword_ViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
