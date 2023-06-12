using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Application.DTOs.UserManager;
using Site.Common.Identity;
using System.Security.Claims;

namespace Site.EndPoint.Controllers
{
   // [Authorize(Policy = "Admin")]

    public class ManageUserController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<IdentityUser> signInManager;
        public ManageUserController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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
            if (User.Identity.Name == user.UserName)//کاربر وارد شده نتواند خودش را ویرایش کند.
            {
                TempData["Message"] = "کاربر وارد شده نمی تواند خودش را ویرایش کند";
                return RedirectToAction("Index");
            }
            var model = new EditUser_ViewModel()
            {
               Id=user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUser_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            user.UserName = model.UserName;
            user.Email = model.Email;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            if (User.Identity.Name==user.UserName)//کاربر وارد شده نتواند خودش را پاک کند.
            {
                TempData["Message"] =  "کاربر وارد شده نمی تواند خودش را پاک کند";
                return RedirectToAction("Index");
            }
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = roleManager.Roles.AsTracking()
                .Select(r => r.Name).ToList();
            var userRoles = await userManager.GetRolesAsync(user);
            var validRoles = roles.Where(r => !userRoles.Contains(r))
                .Select(r => new UserRoles_ViewModel(r)).ToList();
            var model = new AddUserToRole_ViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRole_ViewModel model)
        {
            if (model == null) return NotFound();
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userRoles = await userManager.GetRolesAsync(user);
            var validRoles = userRoles.Select(r => new UserRoles_ViewModel(r)).ToList();
            var model = new AddUserToRole_ViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRole_ViewModel model)
        {
            if (model == null) return NotFound();
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await userManager.RemoveFromRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> AddUserClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var allclaims = ClaimStore.AllClaims;
            var userClaims = await userManager.GetClaimsAsync(user);
            var validclaims = allclaims.Where(c => userClaims.All(l => (l.Type != c.Type)))
                .Select(c => new Claims_ViewModel(c.Type)).ToList();
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
            var validclaims = userClaims.Select(c => new Claims_ViewModel(c.Type)).ToList();
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
    }
}
