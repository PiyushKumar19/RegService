using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using RegService.InterfacesAndSqlRepos;
using RegService.Models;
using RegService.ViewModel;

namespace RegService.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUsersCRUD cRUD;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IUsersCRUD _cRUD, ILogger<AccountController> _logger)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            cRUD = _cRUD;
            this.logger = _logger;
        }


        public IActionResult Index()
        {
            return View(userManager.Users);
        }

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			IdentityUser user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
				if (result.Succeeded)
					return RedirectToAction("Index");
				else
					Errors(result);
			}
			else
				ModelState.AddModelError("", "User Not Found");
			return View("Index", userManager.Users);
		}

		private void Errors(IdentityResult result)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.AddModelError("", error.Description);
		}

		// This method is for registering with Email Id and Password.
		[Route("Account/Developer/")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Account/Developer/")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.ContactNo,
                    TwoFactorEnabled = true
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("GetAllUsers", "Users");

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("GetAllUsers", "Users");
                    else
                    {
                        // log email failed 
                    }
                }
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // This method is for login with Email Id and Password.
        [HttpGet]
        [Route("CAdmin/Login/")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("CAdmin/Login/")]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnURL)
        {
            if (ModelState.IsValid)
            {
                IdentityUser IdentityUser = await userManager.FindByEmailAsync(model.Email);
                if (IdentityUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
                    IdentityUser, model.Password, model.RememberMe, false);
					bool emailStatus = await userManager.IsEmailConfirmedAsync(IdentityUser);
					if (emailStatus == false)
					{
						ModelState.AddModelError(nameof(model.Email), "Email is unconfirmed, please confirm it first");
					}
					if (result.RequiresTwoFactor)
					{
						return RedirectToAction("LoginTwoStep", new { IdentityUser.Email, returnURL });
					}
					if (result.Succeeded)
					{
						if (!string.IsNullOrEmpty(returnURL))
						{
							return Redirect(returnURL);
						}
						else
						{
							return RedirectToAction("Index", "Account");
						}
					}
				}
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }

            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                //return Redirect(returnUrl ?? "/");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View();
            }
        }


        public string GenerateOTP()
        {
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            return r.ToString();
        }

        // This method is for finding the data by FileNo.
        [HttpGet]
        public IActionResult LoginByFormNumber()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginByFormNumber(LoginTestingViewModel model)
        {
            string test = GenerateOTP();

            UsersRegModel user = cRUD.FindByFileNo(model.FileNo);
            if (user == null)
            {
                return NotFound();
            }

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendCodeByLogin(user, test);

            if (emailResponse == true)
            {
                return RedirectToAction("CheckOtpByEmail", new { test, user.Id });
            }
            return PageNotFound();
        }

        [HttpGet]
        public IActionResult CheckOtpByEmail(string test, int id)
        {
            ViewBag.test = test;
            return View();
        }
        [HttpPost]
        public IActionResult CheckOtpByEmail(TwoFactor? twoFactor, string test, int id)
        {
            if (String.Equals(twoFactor.TwoFactorCode, test))
            {
                return RedirectToAction("Details", "Users", new { id });
            }
            return View();
        }

        // This method is for Logout.
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LoginByFormNumber", "Account");
        }

        [HttpGet]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
