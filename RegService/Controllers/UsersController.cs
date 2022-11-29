using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly DatabaseContext context;

        public UsersController(IUsersCRUD _cRUD, DatabaseContext context)
        {
            cRUD = _cRUD;
            this.context = context;
        }

        // This Method is for getting a list of all registered users.
        [Authorize(Roles = "Admin, Developer")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var user = cRUD.GetAll();
            return View(user);
        }

        // This method shows all the details of the user with the Id.
        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = cRUD.GetById(id);

            return View(user);
        }

        // This method is for creating new user.
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // This is the Post Action of Create method.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsersRegModel model)
        {
            if (ModelState.IsValid)
            {                
                var res = cRUD.FindByFileNoAndContactNo(model.FileNo, model.ContactNo);
                if(res == false)
                {
                    var user = cRUD.Create(model);
                    if (user != null)
                    {
                        return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Users", action = "Details", Id = user.Id }));
                    }
                }
            }
            return View();
        }

        // This method is to edit registered user.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var user = cRUD.GetById(id);
            return View(user);
        }
        // Post Action of Edit Method.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UsersRegModel model)
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

        // This method is for deleting the user.
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var model = cRUD.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return RedirectToAction("GetAllUsers");
        }
    }
}
