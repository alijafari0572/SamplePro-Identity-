using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.EndPoint.Models.UserManager
{
    public class EditUser_ViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        //[Remote("IsUserInUse", "Account")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        // [Remote("IsEmailInUse", "Account")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
