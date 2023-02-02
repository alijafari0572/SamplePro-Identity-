using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.EndPoint.Models.Account;

namespace Site.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
               // TempData["user"]
                var login= new Login_ViewModel() { 
                UserNameOrEmail=model.UserName,
                Password=model.Password
                };
                return RedirectToAction("Login", "Account",login);
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, true, true);
            if (result.Succeeded)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword_ViewModel model)
        {
            return View();
        }
    }
}
