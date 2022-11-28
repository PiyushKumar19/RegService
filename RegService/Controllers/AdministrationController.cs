using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegService.ViewModel;

namespace RegService.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            this.roleManager = _roleManager;
            this.userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        // This method is to create new Roles.
        [HttpGet]
        public IActionResult RoleCreation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreation(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("LoginByFormNumber", "Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // This method is to View all the Roles.
        [HttpGet]
        public IActionResult ListRole()
        {
            var role = roleManager.Roles;
            return View(role);
        }

        // This method is to Edit the Roles(Names).
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With ={Id} Cannot Be Found";
                return View("ListRole");
            }
            var model = new RoleEditViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With ID={model.Id} Cannot Be Found";
                return View("ListRole");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }

        // This Method is to add user to a Role or remove users from a Role.
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("ListRole");
            }
            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserID = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("ListRole");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserID);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
