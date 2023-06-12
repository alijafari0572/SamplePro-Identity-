using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Site.Application.DTOs.UserManager
{
    public class IndexUser_ViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
