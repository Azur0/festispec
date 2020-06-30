using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class TemplateViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES
        private InspectionForm _form;
        #endregion

        #region PUBLIC PROPERTIES

        public List<FormQuestionViewModel> Questions = new List<FormQuestionViewModel>();

        public int Id
        {
            get => this._form.Id;
        }

        public string Name
        {
            get => this._form.Name;
            set
            {
                value = value.Trim();
                this._form.Name = (string)Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("FirstName");
            }
        }

        public string CreatedAt
        {
            get => this._form.CreatedAt.ToString();
        }

		#endregion

		#region CONSTRUCTORS
		public TemplateViewModel()
		{
			this._form = new InspectionForm();
		}

		public TemplateViewModel(InspectionForm form)
        {
            this._form = form;
            form.FormQuestion
                .ToList()
                .ForEach(q => this.Questions.Add(new FormQuestionViewModel(q)));
        }
		#endregion

		#region METHODS
		public void LoadTemplateById(int id)
        {
            //QUESTION: Can remove id? Will always get first!
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._form = uow.InspectionFormRepository
                    .GetIncludes("FormQuestion")
                    .First();
            }
        }
        #endregion
    }
}
