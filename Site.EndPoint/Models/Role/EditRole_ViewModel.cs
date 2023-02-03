using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.EndPoint.Models.Role
{
    public class EditRole_ViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام Role")]
        public string Name { get; set; }
    }
}
