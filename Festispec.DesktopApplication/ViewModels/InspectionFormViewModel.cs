using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class InspectionFormViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES
        private InspectionForm _inspection { get; set; }
        #endregion

        #region CONSTRUCTOR
        public InspectionFormViewModel(InspectionForm inspectionForm = null)
        {
            if (inspectionForm == null)
            {
                inspectionForm = new InspectionForm();
            }

            this._inspection = inspectionForm;
            this.FormQuestions = new ObservableCollection<FormQuestionViewModel>();

            if (inspectionForm.FormQuestion != null)
            {
                inspectionForm.FormQuestion.ToList().ForEach(q => this.FormQuestions.Add(new FormQuestionViewModel(q)));
            }

            this.Assignees = new ObservableCollection<UserViewModel>();
        }
        #endregion

        #region PUBLIC PROPERTIES

        public List<int> RemovedQuestions = new List<int>();
        public List<int> RemovedAsingees = new List<int>();

        public ObservableCollection<UserViewModel> Assignees { get; set; }

        public int Id
        {
            get => this._inspection.Id;
        }

        public ObservableCollection<FormQuestionViewModel> FormQuestions { get; set; }

        public string Name
        {
            get => this._inspection.Name;
            set
            {
                value = value.Trim();
                this._inspection.Name = (string)Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("FirstName");
            }
        }

        public void SetInspection(EventInspection eventInspection)
        {
            this._inspection.EventInspectionId = eventInspection.Id;
            this._inspection.EventInspection = eventInspection;
        }

        #endregion

        #region METHODS

        public void LoadInspectionFormByPK(int pk)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._inspection = uow.InspectionFormRepository.GetIncludes("FormQuestion", x => x.Id == pk).First();
                this._inspection.FormQuestion = this._inspection.FormQuestion
                    .Where(x => x.DeletedAt == null)
                    .Select(fq =>
                    {
                        if (fq.Type == "multiple_choice")
                        {
                            fq.Multplechoice = uow.MultipleChoiceRepository.Get().Where(m => m.FormQuestionId == fq.Id).ToList();
                        }
                        return fq;
                    }).ToList();

                this._inspection.FormQuestion.ToList().ForEach(x => this.FormQuestions.Add(new FormQuestionViewModel(x)));
            }
        }

        public void Insert(UnitOfWork unitOfWork = null)
        {
            bool dispose = unitOfWork == null;
            unitOfWork = (unitOfWork == null) ? new UnitOfWork() : unitOfWork;

            unitOfWork.InspectionFormRepository.Insert(this._inspection);
            unitOfWork.Save();

            // Save all the questions
            int order = 0;
            this.FormQuestions.ToList().ForEach(formQuestion => {
                formQuestion.Order = order++;
                formQuestion.InsertOrUpdate(this._inspection.Id, unitOfWork);
            });

            // Attach assignees
            this.Assignees.ToList().ForEach(assignee => {
                unitOfWork.AssigneesRepository.Insert(new Assignees()
                {
                    InspectionFormId = this._inspection.Id,
                    UserId = assignee.Id
                });
            });

            if (dispose)
            {
                unitOfWork.Dispose();
            }
            else
            {
                unitOfWork.Save();
            }
            
        }

        public void Update(UnitOfWork unitOfWork = null)
        {

            //ACTUALLY TRANSCENDED GALAXY BRAIN QUALITY CODE.
            bool dispose = unitOfWork == null;
            unitOfWork = (unitOfWork == null) ? new UnitOfWork() : unitOfWork;

            unitOfWork.InspectionFormRepository.Update(this._inspection);
            unitOfWork.Save();

            // Save all the questions
            int order = 0;
            this.FormQuestions.ToList().ForEach(formQuestion => {
                formQuestion.Order = order++;
                formQuestion.InsertOrUpdate(this._inspection.Id, unitOfWork);
            });

            // remove questions that are deletes
            unitOfWork.FormQuestionRepository
                .Get()
                .Where(fq => this.RemovedQuestions.Contains(fq.Id))
                .ToList()
                .ForEach(fq => unitOfWork.FormQuestionRepository.Delete(fq));
            unitOfWork.Save();

            // remove all assignees that are deletes
            unitOfWork.AssigneesRepository
                .Get()
                .Where(asignee => asignee.InspectionFormId == this._inspection.Id)
                .ToList()
                .ForEach(asingee => unitOfWork.AssigneesRepository.Delete(asingee));
            unitOfWork.Save();

            // Attach assignees
            this.Assignees.ToList().ForEach(assignee => {
                unitOfWork.AssigneesRepository.Insert(new Assignees()
                {
                    InspectionFormId = this._inspection.Id,
                    UserId = assignee.Id
                });
            });

            if (dispose)
                unitOfWork.Dispose();
            else
                unitOfWork.Save();

        }
        #endregion
    }
}
