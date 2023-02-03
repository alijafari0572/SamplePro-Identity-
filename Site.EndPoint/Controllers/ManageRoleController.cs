using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.EndPoint.Models.Role;
using Site.EndPoint.Models.UserManager;

namespace Site.EndPoint.Controllers
{
    public class ManageRoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;

        public ManageRoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddRole_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var role = new IdentityRole(model.Name);
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded) return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
       
        

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();
            var model = new EditRole_ViewModel ()
            {
             Id=id,
             Name=role.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRole_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null) return NotFound();
            role.Name = model.Name;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
    }
}
