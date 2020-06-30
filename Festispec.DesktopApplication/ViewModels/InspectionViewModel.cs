using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using System.Collections.ObjectModel;

namespace Festispec.DesktopApplication.ViewModels
{
    public class InspectionViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES
        private EventInspection _inspection;
        #endregion

        #region CONSTRUCTORS
        public InspectionViewModel(EventInspection inspection = null)
        {
            if (inspection == null)
            {
                inspection = new EventInspection();
            }

            this._inspection = inspection;

            this.InspectionForms = new ObservableCollection<InspectionFormViewModel>();
            this._inspection.InspectionForm
                .ToList()
                .ForEach(form => this.InspectionForms.Add(new InspectionFormViewModel(form)));

            this.AvalibleUsers = new ObservableCollection<AvalibaleUser>();
            this.Events = new List<EventViewModel>();
            this.ExecutionDate = DateTime.Now;
        }
        #endregion

        #region PUBLIC PROPERTIES

        public List<EventViewModel> Events { get; set; }

        public ObservableCollection<InspectionFormViewModel> InspectionForms { get; set; }
        
        public ObservableCollection<AvalibaleUser> AvalibleUsers { get; set; }

        public InspectionFormViewModel SelectedInspectionForm { get; set; }

        public List<TemplateViewModel> Templates { get; set; }

        public List<int> RemovedInspectionForms = new List<int>();

        public int Id
        {
            get => this._inspection.Id;
        }

        private Tuple<object, string> _eventOption { get; set; }
        public Tuple<object, string> EventOption
        {
            get => this._eventOption;
            set
            {
                var selectedValue = (value == null ? null : value.Item1);
                
                if ((selectedValue == null) || (this.EventId.ToString() == selectedValue.ToString()))
                {
                    return;
                }
                this._eventOption = value;
                this.EventId = (int) selectedValue;
                RaisePropertyChanged("EventOption");
            }
        }

        public int EventId
        {
            get => this._inspection.EventId;
            set
            {
                this._inspection.EventId = value;
                this.EventOption = new Tuple<object, string>(value, value.ToString());
                RaisePropertyChanged("EventId");
            }
        }

        public string Name
        {
            get => this._inspection.Name;
            set
            {
                value = value.Trim();
                this._inspection.Name = (string) Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("Name");
            }
        }

        public DateTime ExecutionDate
        {
            get => this._inspection.ExecutionDate;
            set
            {
                this._inspection.ExecutionDate = value;
            }
        }

        #endregion

        #region ACTIONS

        public void AssignUserToForm(InspectionFormViewModel form, int userId)
        {
            if (form.Assignees.Any(u => u.Id == userId)) return;    // dont assign user twice
            form.Assignees.Add(this.AvalibleUsers.Where(user => user.UserViewModel.Id == userId).First().UserViewModel);
        }

        public void LoadEvents()
        {
            UnitOfWork uow = new UnitOfWork();
            uow.EventRepository.GetIncludes("Location")
                .ToList()
                .ForEach(dbEvent => this.Events.Add(new EventViewModel(dbEvent)));
            uow.Dispose();
        }

        public void LoadAvalibleUsers()
        {
            var SelectedEvent = this.Events.Where(e => e.Id == this.EventId).FirstOrDefault();
            if (SelectedEvent == null) return;
            var eventLocation = SelectedEvent.ToModel().Location;

            var googleLocation = new GoogleLocation();

            var avalibleUsers = new List<AvalibaleUser>();

            UnitOfWork uow = new UnitOfWork();
            var Locations = uow.LocationRepository.Get();
            uow.UserRepository.GetIncludes("UserHasAvailability")
                .Where(u => u.UserHasAvailability.Any(av => av.Day.Date == this.ExecutionDate.Date))
                .ToList()
                .ForEach(user => {

                    user.Location = Locations.Where(l => l.Id == user.LocationId).First();

                    var geoloc = new GoogleLocation();

                    var distance = geoloc.GetDistance(
                        eventLocation.Latitude.Value, eventLocation.Longtitude.Value,
                        user.Location.Latitude.Value, user.Location.Longtitude.Value
                    );

                    avalibleUsers.Add( new AvalibaleUser()
                    {
                        distance = distance,
                        UserViewModel = new UserViewModel(user)
                    });
                });
            uow.Dispose();

            AvalibleUsers.Clear();

            // Add sorted and filter list
            avalibleUsers
                .OrderBy(x => x.distance)
                .ToList()
                .ForEach(x => AvalibleUsers.Add(x));
        }

        public void LoadTemplates()
        {
            Templates = new List<TemplateViewModel>();

            UnitOfWork unitOfWork = new UnitOfWork();

            // load all templates
            var templates = unitOfWork.InspectionFormRepository.Get().Where(form => form.EventInspectionId == null).ToList();

            // load all questions for each template
            templates.ToList().ForEach(template =>
            {
                template.FormQuestion = unitOfWork.FormQuestionRepository.GetIncludes("Multplechoice", q => q.InspectionId == template.Id).ToList();
            });

            this.Templates = templates.Select(form => new TemplateViewModel(form)).ToList();

            unitOfWork.Dispose();
        }

        public void LoadInspectionByPK(int pk)
        {
            UnitOfWork uow = new UnitOfWork();
            this._inspection = uow.EventInspectionRepository.Get().ToList().Where(x => x.Id == pk).First();
            this.EventId = this.EventId;

            // load inspection forms
            this._inspection.InspectionForm = uow.InspectionFormRepository.Get().Where(form => form.EventInspectionId == pk).ToList();

            // load questions for each form
            this._inspection.InspectionForm.ToList().ForEach(form =>
            {
                form.FormQuestion = uow.FormQuestionRepository.GetIncludes("Multplechoice", q => q.InspectionId == form.Id).ToList();
                form.Assignees = uow.AssigneesRepository.Get().Where(asingee => asingee.InspectionFormId == form.Id).ToList();
            });

            this._inspection.InspectionForm.ToList().ForEach(form =>
            {
                var formVM = new InspectionFormViewModel(form);
                uow.UserRepository
                    .Get()
                    .Where(u => form.Assignees.Any(a => a.UserId == u.Id))
                    .Select(u => { u.Password = null; return new UserViewModel(u); } )
                    .ToList()
                    .ForEach(userVM => formVM.Assignees.Add(userVM));

                this.InspectionForms.Add(formVM);
            });

            uow.Dispose();
        }

        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.EventInspectionRepository.Insert(this._inspection);

                unitOfWork.Save();

                this.InspectionForms.ToList().ForEach(formVM =>
                {
                    formVM.SetInspection(this._inspection);
                    formVM.Insert(unitOfWork);
                });
                unitOfWork.Save();
            }
        }

        public void Update()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.EventInspectionRepository
                    .Update(this._inspection);
                
                unitOfWork.Save();

                // delete all removed forms
                this.RemovedInspectionForms.ForEach(deletedFormId =>
                {
                    EventInspection formToDelete = unitOfWork.EventInspectionRepository
                        .Get()
                        .Where(x => x.Id == deletedFormId)
                        .First();
                    
                    unitOfWork.EventInspectionRepository
                        .Delete(formToDelete);
                    
                    unitOfWork.Save();
                });

                this.InspectionForms.ToList().ForEach(formVM =>
                {
                    formVM.SetInspection(this._inspection);
                    if (formVM.Id == 0)
                    {
                        formVM.Insert(unitOfWork);
                    }
                    else
                    {
                        formVM.Update(unitOfWork);
                    }
                });

                unitOfWork.Save();
            }
        }

        #endregion

    }

    public struct AvalibaleUser
    {
        public UserViewModel UserViewModel { get; set; }
        public double distance { get; set; }
    }
}
