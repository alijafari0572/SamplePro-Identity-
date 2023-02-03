using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Common.Identity;
using Site.EndPoint.Models.UserManager;
using System.Security.Claims;

namespace Site.EndPoint.Controllers
{
    [Authorize(Policy = "Admin")]

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
                Id = i.Id,
                UserName = i.UserName,
                Email = i.Email
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, string userName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(userName)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.UserName = userName;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
        public async Task<IActionResult> AddUserClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var allclaims = ClaimStore.AllClaims;
            var userClaims = await userManager.GetClaimsAsync(user);
            var validclaims = allclaims.Where(c => userClaims.All(l => (l.Type != c.Type))).Select(c => new ClaimsViewModel(c.Type)).ToList();
            var model = new AddorRemoveUserClaims_ViewModel(id, validclaims);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserClaim(AddorRemoveUserClaims_ViewModel model)
        {
            if (model == null) return NotFound();
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestClaims =
                model.UserClaims.Where(r => r.IsSelected)
                .Select(u => new Claim(u.ClaimType, true.ToString())).ToList();

            var result = await userManager.AddClaimsAsync(user, requestClaims);

            if (result.Succeeded) return RedirectToAction("index");

            return View(model);
        }

        public async Task<IActionResult> RemoveUserClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userClaims = await userManager.GetClaimsAsync(user);
            var validclaims = userClaims.Select(c => new ClaimsViewModel(c.Type)).ToList();
            var model = new AddorRemoveUserClaims_ViewModel(id, validclaims);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserClaim(AddorRemoveUserClaims_ViewModel model)
        {
            if (model == null) return NotFound();
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestClaims =
                model.UserClaims.Where(r => r.IsSelected)
                    .Select(u => new Claim(u.ClaimType, true.ToString())).ToList();

            var result = await userManager.RemoveClaimsAsync(user, requestClaims);

            if (result.Succeeded) return RedirectToAction("index");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

    }
}
