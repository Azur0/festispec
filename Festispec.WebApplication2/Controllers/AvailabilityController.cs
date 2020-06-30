using Festispec.WebApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication2.Controllers
{
    public class AvailabilityController : Controller
    {
        // GET: Availability
        public ActionResult Index()
        {
            if(Session["userID"] == null)
                return RedirectToAction("Incex", "Account");

            List<UserHasAvailability> Availability;

            using (UnitOfWork uow = new UnitOfWork())
            {
                Availability = uow.UserHasAvailibiltyRepository
                    .Get()
                    .Where(x => x.UserId == (int)Session["userID"])
                    .ToList();

                return View(new AvailabilityViewModel()
                { 
                    Dates = Availability 
                });
            }
        }

        [HttpPost]
        public ActionResult Index(DateTime dateTime)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            UserHasAvailability exists;

            using (UnitOfWork uow = new UnitOfWork())
            {
                exists = uow.UserHasAvailibiltyRepository
                        .Get()
                        .Where(x => (x.UserId == (int)Session["userID"]) && (x.Day == dateTime))
                        .FirstOrDefault();

                if (exists == null)
                {
                    uow.UserHasAvailibiltyRepository.Insert(new UserHasAvailability()
                    {
                        UserId = (int)Session["userID"],
                        Day = dateTime
                    });
                }
                else
                {
                    uow.UserHasAvailibiltyRepository.Delete(exists);
                }

                uow.Save();
            }
            return null;
        }
    }
}