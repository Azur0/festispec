using Festispec.WebApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication2.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            User user;

            using (UnitOfWork uow = new UnitOfWork())
            {
                user = uow.UserRepository
                    .Get(u => u.Id == (int)Session["userID"])
                    .FirstOrDefault();
            }

            if (user.MiddleName != null) user.MiddleName = $" {user.MiddleName}";
            if (user.LastName != null) user.LastName = $" {user.LastName}";

            return View(new UserViewModel() 
            { 
                Name = user.FirstName + user.MiddleName + user.LastName, 
                Email = user.Email 
            });
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Email, Password, ConfirmPassword")] UserViewModel model)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");


            User tempUserObject, temp;

            using (UnitOfWork uow = new UnitOfWork())
            {
                tempUserObject = uow.UserRepository
                    .Get(u => u.Id == (int)Session["userID"])
                    .FirstOrDefault();

                model.Name = $"{tempUserObject.FirstName} {tempUserObject.LastName}";

                if (!ModelState.IsValid)
                    return View(model);
                
                try
                {
                    temp = uow.UserRepository
                        .Get(u => u.Id == (int)Session["userID"])
                        .FirstOrDefault();

                    temp.Email = model.Email;
                    temp.Password = Statics.SHA256(model.Password);

                    uow.UserRepository
                        .Update(temp);
                    
                    uow.Save();
                }
                catch (Exception)
                {
                    // Nothing to see here ( ͡° ͜ʖ ͡°)
                }

                return View(model);
            }
        }      
    }
}