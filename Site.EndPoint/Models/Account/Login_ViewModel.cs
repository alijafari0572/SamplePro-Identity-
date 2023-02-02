using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.EndPoint.Models.Account
{
    public class Login_ViewModel
    {

        [Display(Name = " نام کاربری یا ایمیل")]
        [Required(ErrorMessage = "لطفا نام کاربری یا ایمیل را وارد کنید")]
        [MinLength(3, ErrorMessage = "حداقل تعداد کاراکتر 3 است")]
        [MaxLength(50, ErrorMessage = "بیشترین تعداد کاراکتر 50 است")]
        public string UserNameOrEmail { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "به خاطر سپاری رمز عبور ؟")]
        public bool RememberMe { get; set; }
    }
}
