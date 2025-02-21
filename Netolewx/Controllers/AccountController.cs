using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Netolewx.Controllers;
using Netolex.ViewModels.Account;
using RouteCourse.Services.EmailSender;
using System.ComponentModel.DataAnnotations;

namespace Netolex.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IEmailSender emailSender,IConfiguration configuration)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _emailSender=emailSender;
            _configuration=configuration;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                if (ModelState.IsValid)
                {
                    //افتكر ان انت هترجع للبروجرام وتكتب ليهم اللي محتاجينه
                    user = new ApplicationUser
                    {
                        UserName = model.Username,
                        FName = model.FName,
                        LName = model.LName,
                        Email = model.Email,
                        AgreeToTerms=model.AgreeToTerms,
                    };
                    var result = await _userManager.CreateAsync(user,model.Password);
                    if(result.Succeeded)
                        return RedirectToAction("SignIn");
                    foreach(var item in result.Errors)
                        ModelState.AddModelError("", item.Description);

                }
                ModelState.AddModelError("", "this username is already Registered!");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (ModelState.IsValid)
            {
                // التحقق مما إذا كان المدخل بريدًا إلكترونيًا أو اسم مستخدم
                var isEmail = new EmailAddressAttribute().IsValid(model.EmailOrUsername);
                var user = isEmail
                    ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                    : await _userManager.FindByNameAsync(model.EmailOrUsername);

                if (user != null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Your account is locked due to multiple failed attempts.");
                        }
                        else if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                        else if (result.IsNotAllowed)
                        {
                            ModelState.AddModelError("", "Your account is not allowed to sign in.");
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public   IActionResult ForgotPassword()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    // ✅ توليد رمز إعادة تعيين كلمة المرور
                    var ResetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // ✅ إنشاء رابط إعادة التعيين بطريقة صحيحة
                    var UrlResetEmailPassword = Url.Action("ResetPassword", "Account",
                        new { email = user.Email, token = ResetPasswordToken },
                        protocol: Request.Scheme);

                    // ✅ إرسال الإيميل مع الرابط الصحيح
                    await _emailSender.SendAsync(
                        from: _configuration["EmailSettings:SenderEmail"],
                        reciptients: model.Email,
                        subject: "Reset Your Email Password!",
                        body: $"<p>Click <a href='{UrlResetEmailPassword}'>here</a> to reset your password.</p>");

                    return RedirectToAction("CheckURInbox");
                }
                ModelState.AddModelError("", "No registered account!!");
            }
            return View();
        }

        public IActionResult CheckURInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"]=email;
            TempData["Token"]=token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var email = TempData["Email"] as string;
            var token = TempData["Token"] as string;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    return RedirectToAction(nameof(SignIn));
                }
            }

            return View(model);
        }

       
    }
}
