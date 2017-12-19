using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CIMOBProject.Models;
using CIMOBProject.Models.AccountViewModels;
using CIMOBProject.Services;
using CIMOBProject.Data;
using Microsoft.EntityFrameworkCore;


namespace CIMOBProject.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            loadHelp();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");                    
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Dados inválidos ou a conta não se encontra verificada.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["CollegeSubjectId"] = new SelectList(_context.CollegeSubjects, "Id", "SubjectAlias");
            loadHelp();
            return View();
        }

        private void loadHelp()
        {
            ViewData["EmailTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 2)as Help).HelpDescription;
            ViewData["PasswordTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 3) as Help).HelpDescription;
            ViewData["ConfirmPasswordTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 4) as Help).HelpDescription;
            ViewData["UserNameTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 1) as Help).HelpDescription;
            ViewData["BirthDateTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 8) as Help).HelpDescription;
            ViewData["UserCcTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 9) as Help).HelpDescription;
            ViewData["PhoneNumberTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 5) as Help).HelpDescription;
            ViewData["UserAddressTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 6) as Help).HelpDescription;
            ViewData["PostalCodeTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 7) as Help).HelpDescription;
            ViewData["StudentNumberTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 10) as Help).HelpDescription;
            ViewData["CollegeSubjectTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 11) as Help).HelpDescription;
        }


        ///<summary>
        ///In this method we create the various students that will use our website.
        ///Even thou this method was created by the MVC we had to alter it a bit to suite our needs.
        ///One of the changes made was the attribution of roles do the students. With this we control who gets access to the various uses of our application.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["CollegeSubjectId"] = new SelectList(_context.CollegeSubjects, "Id", "SubjectAlias");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Student
                {
                    UserName = model.Email,
                    UserFullname = model.UserName,
                    Email = model.Email,
                    UserCc = model.UserCc,
                    PhoneNumber = model.PhoneNumber,
                    UserAddress = model.UserAddress,
                    PostalCode = model.PostalCode,
                    BirthDate = model.BirthDate,
                    CollegeSubjectId = model.CollegeSubjectId,
                    StudentNumber = model.StudentNumber};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var role = _context.Roles.SingleOrDefault(m => m.Name == "Student");
                    await _userManager.AddToRoleAsync(user, role.Name);
                    _context.SaveChanges();
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    //Comment this to disable login after register
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    //return RedirectToLocal(returnUrl);
                    return View("AskEmailConfirmation");          
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            ViewData["UserFullName"] = "";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            loadHelp();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user))) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reiniciar Password",
                   $"Se solicitou um reinicio de password, visite este link: <a href='{callbackUrl}'>link</a>." +
                   $"<br>Senão, pode ignorar este email.");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A reposição de password precisa de um código.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
