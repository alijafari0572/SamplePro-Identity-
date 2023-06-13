using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Site.Application.Contracts.Infrastructure;
using Site.Application.DTOs.Account;
using Site.Infrastructure.GenerateCode;
using Site.Infrastructure.Mail;

namespace Healthy.Controllers
{

    [Route("api/authentication")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register_ViewModel model)
        {

            if (signInManager.IsSignedIn(User))
                return Ok(new { res = 0, msg = "Error", err = "You are Singin" });

            if (!ModelState.IsValid)
            {
                return Ok(new { res = 0, msg = "Error", err = "Please Correct Enter Value." });
            }
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // var emailConfirmationToken =
                // await userManager.GenerateEmailConfirmationTokenAsync(user);
                //  var emailMessage =
                // Url.Action("ConfirmEmail", "Account",
                //  new { username = user.UserName, token = emailConfirmationToken },
                //  Request.Scheme);
                //await _messageSender.SendEmailAsync(model.Email, "Email confirmation", emailMessage);
                return Ok(new { res = 1, msg = "Account Created", err = "" });
            }
            return Ok(new { res = 0, msg = "Error", err = result.Errors.ToList() });
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(Login_ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { res = 0, msg = "Error", err = "Email or Password is wrong." });

            }
            var user = model.UserNameOrEmail;
            if (model.UserNameOrEmail.Contains("@"))
            {
                var useremail = await userManager.FindByEmailAsync(model.UserNameOrEmail);
                if (useremail != null)
                {
                    user = useremail.UserName;
                }
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (result.Succeeded)
            {
                return Ok(new { res = 1, msg = "Login", err = "" });

            }
            return Ok(new { res = 0, msg = "Error", err = "Email or Password is wrong." });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok(new { res = 1, msg = "Logout", err = "" });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var loginViewModel = new Login_Model()
                //{
                //    ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                //};

                //  ViewData["ErrorMessage"] = "اگر ایمیل وارد معتبر باشد، لینک فراموشی رمزعبور به ایمیل شما ارسال شد";
                Random _random = new Random();
                var code=new Random().Next(100000,999999);
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null) return Ok(new { res = 1, msg = "Email Sent", err = "" });
                var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
                //string resetPasswordUrl = Url.Page("/Identity/Account/ResetPassword", pageHandler: null,values : 
                  //  new { code },protocol : Request.Scheme , "localhost:7012");

               var result= await userManager.ResetPasswordAsync(user, resetPasswordToken,code.ToString());
                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(user.Email, "You'er new Password", code.ToString());
                }
                //return View("Login", loginViewModel);
            }
            return Ok();

        }
        //[HttpGet]
        //public IActionResult ResetPassword(string email, string token)
        //{
        //    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
        //        return RedirectToAction("Index", "Home");
        //    var model = new ResetPassword_ViewModel()
        //    {
        //        Email = email,
        //        Token = token
        //    };

        //    return View(model);
        //}

    }
}
