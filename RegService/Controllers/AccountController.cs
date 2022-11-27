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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
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
                var newuser = new UsersRegModel
                {
                    Name = model.Name,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    ContactNo = model.ContactNo
                };
                cRUD.Create(newuser);
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("AllProducts", "Products");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Register", "Account");
        }
    }
}
