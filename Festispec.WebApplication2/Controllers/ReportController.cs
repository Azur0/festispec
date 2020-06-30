using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festispec.WebApplication2.ViewModels;

namespace Festispec.WebApplication2.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index(int id, ReportViewModel model)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                model.Event = uow.EventRepository
                    .GetIncludes("EventInspection.InspectionForm.FormQuestion.Answer", x => x.Id == id)
                    .FirstOrDefault();
            }

            model.EventForms = model.Event.EventInspection
                    .FirstOrDefault()
                    .InspectionForm
                    .ToList();

            return View(model);
        }
    }
}