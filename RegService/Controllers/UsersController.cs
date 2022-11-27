using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegService.AppDbContext;
using RegService.InterfacesAndSqlRepos;
using RegService.Models;

namespace RegService.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersCRUD cRUD;

        public UsersController(IUsersCRUD _cRUD)
        {
            cRUD = _cRUD;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = cRUD.GetAll();
              return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            cRUD.Delete(id);

            return RedirectToAction("GetAllUsers");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsersRegModel model)
        {
            if (ModelState.IsValid)
            {
                var user = cRUD.Create(model);
                if(user != null)
                {
                    return RedirectToAction("GetAllUsers");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = cRUD.Get(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherName,MotherName,PhoneNo,Email")] UsersRegModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                cRUD.Update(model);
                return RedirectToAction(nameof(GetAllUsers));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await cRUD.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return RedirectToAction("GetAllUsers");
        }
    }
}
