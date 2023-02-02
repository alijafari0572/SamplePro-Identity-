using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Common.Identity;
using Site.EndPoint.Models.UserManager;

namespace Site.EndPoint.Controllers
{
    public class ManageUserController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public ManageUserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = userManager.Users.Select(i => new IndexUser_ViewModel()
            {
                Id=i.Id,
                UserName = i.UserName,
                Email = i.Email
            }).ToList();
           
            return View(model);
        }
        public async Task<IActionResult> AddUserClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var allclaims = ClaimStore.AllClaims;
            var userClaims=await userManager.GetClaimsAsync(user);
            var validclaims = allclaims.Except(userClaims).Select(C => new ClaimsViewModel(C.Type)).ToList(); ;
            var model = new AddorRemoveUserClaims_ViewModel(id, validclaims);
            return View(model);
        }
    }
}
