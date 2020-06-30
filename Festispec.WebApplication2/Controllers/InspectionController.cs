using Festispec.WebApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication2.Controllers
{
    public class InspectionController : Controller
    {
        // GET: Inspection
        public ActionResult Index(InspectionFormViewModel model)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            Assignees temp, answers, eventdetails;

            using (UnitOfWork uow = new UnitOfWork())
            {
                temp = uow.AssigneesRepository
                    .GetIncludes("InspectionForm.FormQuestion.Multplechoice", u => u.UserId == (int) Session["userID"] && u.InspectionForm.Id == model.Id)
                    .FirstOrDefault();

                answers = uow.AssigneesRepository
                    .GetIncludes("InspectionForm.FormQuestion.Answer", u => u.UserId == (int)Session["userID"] && u.InspectionForm.Id == model.Id)
                    .FirstOrDefault();

                eventdetails = uow.AssigneesRepository
                    .GetIncludes("InspectionForm.EventInspection.Event", u => u.UserId == (int)Session["userID"] && u.InspectionForm.Id == model.Id)
                    .FirstOrDefault();

            }

            answers.InspectionForm.FormQuestion = answers.InspectionForm.FormQuestion.Select(formquestion =>
            {
                formquestion.Answer = formquestion.Answer
                    .Where(answer => answer.UserId == (int)Session["userID"])
                    .ToList();

                return formquestion;
            }).ToList();

            InspectionFormViewModel inspectionFormViewModel = new InspectionFormViewModel() 
            { 
                Id = temp.InspectionForm.Id, FormQuestion = temp.InspectionForm.FormQuestion.ToList(),
                EventName = $"{temp.InspectionForm.EventInspection.Event.Name} : {temp.InspectionForm.Name}" 
            };

            return View(inspectionFormViewModel);
        }

        public ActionResult Overview(InspectionFormViewModel model)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");
            
            if (model.Id == 0) 
                return RedirectToAction("Index", "Home");

            InspectionForm temp, answers;

            using (UnitOfWork uow = new UnitOfWork())
            { 
                temp = uow.InspectionFormRepository
                    .GetIncludes("FormQuestion.Answer", u => u.Id == model.Id)
                    .FirstOrDefault();
                
                answers = uow.InspectionFormRepository
                    .GetIncludes("FormQuestion.Answer", u => u.Id == model.Id)
                    .FirstOrDefault();
            }

            model.FormQuestion = temp.FormQuestion.ToList();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Overview(InspectionFormViewModel model, FormCollection formValues)
        {
            //QUESTION: Possible to remove FormCollection?

            try
            {
                Assignees temp;
                int questionsAmount, answeredQuestionsAmount;

                using (UnitOfWork uow = new UnitOfWork())
                { 
                    temp = uow.AssigneesRepository
                        .GetIncludes("InspectionForm.FormQuestion.Answer", x => x.UserId == (int)Session["userID"] && x.InspectionFormId == model.Id)
                        .FirstOrDefault();
                    
                    questionsAmount = temp.InspectionForm.FormQuestion
                        .Count();
                    
                    answeredQuestionsAmount = temp.InspectionForm.FormQuestion
                        .Where(question => question.Answer.Count() > 0)
                        .Count();

                    if (questionsAmount == answeredQuestionsAmount)
                        temp.Completed = true;
                }
            }
            catch(Exception)
            {
                //
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, InspectionFormViewModel model)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            var saveType = Int32.Parse(form["SaveType"]);

            form.Remove("SaveType");

            UnitOfWork uow = new UnitOfWork();

            var temp = uow.InspectionFormRepository.GetIncludes("FormQuestion.Answer", u => u.Id == model.Id).FirstOrDefault();
            var images = Request.Files;

            // Images
            foreach(var imageKey in images.AllKeys)
            {
                foreach(var questionz in temp.FormQuestion)
                {
                    if (Int32.Parse(imageKey.ToString().Replace("question", "")) == questionz.Id)
                    {
                        var filetype = images[imageKey];

                        if (images[imageKey].FileName != String.Empty && images[imageKey].ContentType.Contains("image"))
                        {
                            // Create local image storage if folder doesnt exist
                            Directory.CreateDirectory(Server.MapPath("~/Content/form" + model.Id));
                            var filename = Path.Combine(Server.MapPath("~/Content/form" + model.Id + "/"), images[imageKey].FileName);
                            images[imageKey].SaveAs(filename);
                            filename = $"~/Content/form{model.Id}/{images[imageKey].FileName}";

                            // Write to database
                            var existingRecord = uow.AnswerRepository.Get(u => u.UserId == (int)Session["userID"] && u.FormQuestionId == Int32.Parse(imageKey.ToString().Replace("question", "")));

                            if (existingRecord.Count() == 0)
                            {
                                uow.AnswerRepository.Insert(new Answer() { UserId = (int)Session["userID"], Value = filename, FormQuestionId = Int32.Parse(imageKey.ToString().Replace("question", "")) });
                            }
                            else
                            {
                                existingRecord.First().Value = filename;
                                uow.AnswerRepository.Update(existingRecord.First());
                            }
                        }
                    }
                }
            }

            // Textbox & multiple choice
            foreach(var key in form.AllKeys)
            {
                var answer = form[key];
                if (answer.ToString() != String.Empty)
                {
                    foreach (var questionx in temp.FormQuestion)
                    {
                        if (questionx.Id == Int32.Parse(key.ToString().Replace("question", "")))
                        {
                            var existingRecord = uow.AnswerRepository.Get(u => u.UserId == (int)Session["userID"] && u.FormQuestionId == questionx.Id);

                            if (existingRecord.Count() == 0)
                            {
                                uow.AnswerRepository.Insert(new Answer() { UserId = (int)Session["userID"], Value = answer, FormQuestionId = questionx.Id });
                            }
                            else
                            {
                                existingRecord.First().Value = answer;
                                uow.AnswerRepository.Update(existingRecord.First());
                            }
                        }
                    }
                }
            }

            uow.Save();
            uow.Dispose();

            if(saveType == 1)
            {
                return RedirectToAction("Overview", "Inspection", model);
            }
            else
            {
                return RedirectToAction("Index", "Inspection");
            }
        }
    }
}