using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegService.AppDbContext;
using RegService.InterfacesAndSqlRepos;
using RegService.Models;
using RegService.ViewModel;

namespace RegService.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersCRUD cRUD;
        private readonly DatabaseContext context;
        private readonly IConfiguration Configuration;

        public UsersController(IUsersCRUD _cRUD, DatabaseContext context, IConfiguration configuration)
        {
            cRUD = _cRUD;
            this.context = context;
            this.Configuration = configuration;
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

            if (user.Errors != null)
            {
                return RedirectToAction(nameof(ShowErrors));
            }
            return View(user);
        }

        public IActionResult ShowErrors(UsersRegModel model)
        {
            return View(model);
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
                if (res == false)
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

        [HttpGet]
        public IActionResult TransferFile(int id)
        {
            var user = cRUD.GetById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult TransferFile([Bind("Id,FileNo,FirstName,LastName,ContactNo,EmailId,Address,Pincode,State,District,AccountNo,IFSCcode,Branch,BankName,AadhaarNo,PanNo")] UsersRegModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersRegistered
                {
                    FileNo = model.FileNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ContactNo = model.ContactNo,
                    EmailId = model.EmailId,
                    Address = model.Address,
                    Pincode = model.Pincode,
                    State = model.State,
                    District = model.District,
                    AccountNo = model.AccountNo,
                    IFSCcode = model.IFSCcode,
                    Branch = model.Branch,
                    BankName = model.BankName,
                    AadhaarNo = model.AadhaarNo,
                    PanNo = model.PanNo
                };
                if (user != null)
                {
                    if (ModelState.IsValid)
                    {
                        context.UsersRegistered.Add(user);
                        context.SaveChanges();
                        return RedirectToAction(nameof(SuccessfulSubmit));
                    }
                }
            }
            return View();
        }

        public IActionResult SuccessfulSubmit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RaiseQuery()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccountDetailsFill()
        {
            return View();
        }

        public IActionResult AccountDetailsFill(int id, [Bind("AccountNo, IFSCcode, Branch, BankName, AadhaarNo, PanNo")] AccountDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsersRegModel user = cRUD.GetById(id);
                user.AccountNo = model.AccountNo;
                user.IFSCcode = model.IFSCcode;
                user.Branch = model.Branch;
                user.BankName = model.BankName;
                user.AadhaarNo = model.AadhaarNo;
                user.PanNo = model.PanNo;

                cRUD.Update(user);
                return RedirectToAction("Details", new {Id = user.Id});
            }
            return View();
        }
    }
}
