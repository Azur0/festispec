using Festispec.WebApplication.ViewModels;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            DashBoardViewModel dashBoardViewModel = new DashBoardViewModel(Session["userID"]);
            List<Assignees> assigneeList = dashBoardViewModel.FetchAssignedInspections().ToList();

            return View(assigneeList);
        }
    }
}