using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.Application.DTOs.Role
{
    public class AddRole_ViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام Role")]
        public string Name { get; set; }
    }
}
