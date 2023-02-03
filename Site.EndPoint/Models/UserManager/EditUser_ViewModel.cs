using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.EndPoint.Models.UserManager
{
    public class EditUser_ViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
