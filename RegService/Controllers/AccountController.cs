using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("GetAllUsers", "Users");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
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
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        // This method is for finding the data by FileNo.
        //[Route("Account/Login/")]
        [HttpGet]
        public IActionResult LoginByFormNumber()
        {
            return View();
        }
        //[Route("Account/Login/")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginByFormNumber(LoginTestingViewModel model)
        {
            UsersRegModel user = cRUD.FindByFileNo(model.FileNo);
            if (user == null)
            {
                return NotFound();
            }
            return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Users", action = "Details", Id = user.Id }));
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
