using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RugerRumble.Models;
using RugerRumble.Models.ViewModel.AccountViewModel;
using RugerRumble.Services;

namespace RugerRumble.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        //inject
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        //end inject
        // start login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            //returnUrl = returnUrl.Replace("%2F", "/");

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {
                
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (login.ReturnUrl == null)
                    {
                        
                            return RedirectToAction("Index", "Home");
                        
                    }
                    else
                    {
                        return Redirect(login.ReturnUrl);
                    }
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("IncorrectInput", "Email is not verified.");
                    return View(login);

                }
            }
            ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
            return View(login);
        }

        //end login
        //start registration
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {

            if (_signInManager.IsSignedIn(User) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_signInManager.IsSignedIn(User) && !User.IsInRole("Client"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration, RegisterViewModel register)
        {
            var isValid = IsValidEmail(register.Email);
            if (isValid == false)
            {
                ModelState.AddModelError("Email", "Email is not valid!");
            }
            else
            {
                var usercheck = await _userManager.FindByEmailAsync(register.Email ?? "");
                if (usercheck != null)
                {
                    ModelState.AddModelError("Email", "Email is already exists!");
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    SetDataToApplicationUser(ref user, ref register);
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Client");
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var link = Url.Action(nameof(VerifyEmail), "Account", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
                        emailSender.Post(
                           subject: "Ablice: E-mail verification",
                           body: $"<div><h4><strong>Welcome to ablice</strong></h4><br/><p>Please click on the link to verify your account.</p><br/><p>{link} </p><br/><p> or <button  class=\"btn btn-success\"><a href=\"{link}\">Click Here</a></button></p></div>",
                           recipients: user.Email,
                           sender: configuration["AdminContact"]);
                        return RedirectToAction(nameof(EmailVerification));

                    }
                }
            }
            return View(register);
        }
        private object SetDataToApplicationUser(ref ApplicationUser user, ref RegisterViewModel register)
        {
            user.Email = register.Email;
            user.UserName = register.Email;
            return user;
        }
        public async Task<IActionResult> VerifyEmail(string userID, string code)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult EmailVerification()
        {
            return View();
        }
        //end registration

        //update profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return View(usr);
        }
        [Authorize]
        public async Task<IActionResult> editprofile()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return View(usr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfileConfirm(ApplicationUser updateInfo)
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            //usr.FullName = updateInfo.FullName;
            //usr.DOB = updateInfo.DOB;
            //usr.GenderID = updateInfo.GenderID;
            //usr.GroupID = updateInfo.GroupID;
            //usr.DonateCount = updateInfo.DonateCount;
            //usr.PhoneNumber = updateInfo.PhoneNumber;
            //usr.PhoneSecondary = updateInfo.PhoneSecondary;
            //usr.CityID = updateInfo.CityID;
            //usr.Area = updateInfo.Area;
            //usr.Remarks = updateInfo.Remarks;
            var successs = await _userManager.UpdateAsync(usr);
            if (successs.Succeeded)
            {
                return RedirectToAction("Profile");
            }

            return RedirectToAction("Profile");
        }
        //edit profile
        // forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordMail([FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration, RecoverVM model)
        {
            var validatemail = IsValidEmail(model.Email);
            if (validatemail == false)
            {
                ModelState.AddModelError("Email", "Email is not valid");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Account does not exists.");
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action(nameof(ResetPassword), "Account", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
                    emailSender.Post(
                       subject: "Ablice: Reset Password",
                       body: $"<div><p>Please click on the link to reset your password.</p><br/><p>{link} </p><br/><p> or <button  class=\"btn btn-success\"><a href=\"{link}\">Click Here</a></button></p></div>",
                       recipients: user.Email,
                       sender: configuration["AdminContact"]);
                    return RedirectToAction(nameof(ResetMailSent));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetMailSent()
        {
            return View();
        }
        public IActionResult ResetPassword(string userID, string code)
        {
            PasswordVM recover = new PasswordVM();
            recover.UserID = userID;
            recover.BaseCode = code;
            return View(recover);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(PasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserID);
                var result = await _userManager.ResetPasswordAsync(user, model.BaseCode, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                }
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }
        //forgot password
        //change password
        public async Task<IActionResult> ChangePassword(PasswordVM model)
        {
            //PasswordVM model = new PasswordVM();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            model.UserID = user.Id;
            model.Email = user.Email;
            model.BaseCode = token;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordConfirm(PasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserID);
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    model.Errormessage = "Password didnot match";
                    return View("ChangePassword", model);
                }
            }
            return View("ChangePassword", model);
        }
        //logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        //Get loggedin user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        //mail checker
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
