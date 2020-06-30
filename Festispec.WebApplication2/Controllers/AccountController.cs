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
    public class AccountController : Controller
    {
        public ActionResult Index(LoginViewModel model) => View();
        
        public ActionResult Login(LoginViewModel model)
        {
            if(model.Email == null || model.Password == null)
                return RedirectToAction("Index", "Account");
              //return View("~/Views/Account/Index.cshtml");
            
            model.Password = SHA256(model.Password);

            User userDetails;

            using (UnitOfWork uow = new UnitOfWork())
            { 
                userDetails = uow.UserRepository.Get(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
            }

            if (userDetails == null)
            {
                model.LoginErrorMessage = "Verkeerde email/wachtwoord";
                return View("Index", model);
            }
            else
            {
                Session["userID"] = userDetails.Id;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Account");
        }

        private static string SHA256(string text)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            crypt.Dispose();
            
            return hash.ToString();
        }
    }
}