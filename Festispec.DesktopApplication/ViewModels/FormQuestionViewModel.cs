using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
	public class FormQuestionViewModel : ViewModelBase
	{
		#region PRIVATE PROPERTIES
		private FormQuestion _formQuestion;
        private List<string> _multplechoices;
		#endregion
		
		#region CONSTRUCTOR
		public FormQuestionViewModel(FormQuestion formQuestion = null)
		{
			if(formQuestion == null)
			    formQuestion = new FormQuestion();
            

			this._formQuestion = formQuestion;
            this._multplechoices = formQuestion.Multplechoice.Select(x => x.Option).ToList();

        }
        #endregion

        #region PUBLIC PROPERTIES

        public FormQuestion GetModel() {
            return this._formQuestion;
        }

        public int Id
		{
			get => this._formQuestion.Id;
		}

        public List<string> MultpleChoices
        {
            get => this._multplechoices;
            set => this._multplechoices = value;
        }

        private Tuple<object, string> _questionTypeOption { get; set; }
		public Tuple<object, string> QuestionTypeOption
		{
			get => this._questionTypeOption;
			set
			{
                if (value == null) return;
				if(( this._questionTypeOption == null ? null : this._questionTypeOption.Item1 ) == value.Item1)
					return;
				this._questionTypeOption = value;
				this.QuestionType = (string)_questionTypeOption.Item1;
				RaisePropertyChanged("QuestionTypeOption");
			}
		}

        public string QuestionType
        {
            get => this._formQuestion.Type;
            set
            {
                value = value.Trim();
                value = (string)Validators.Check(value, "Type", "NotEmpty|MaxLength:45");
                this._formQuestion.Type = value;
                this.QuestionTypeOption = new Tuple<object, string>(value, value);
                RaisePropertyChanged("QuestionType");
            }
        }

        public string Question
        {
            get => this._formQuestion.Question;
            set
            {
                value = value.Trim();
                this._formQuestion.Question = (string)Validators.Check(value, "Vraag", "NotEmpty|MaxLength:255");
                RaisePropertyChanged("Question");
            }
        }

        public string Instructions
        {
            get => this._formQuestion.Instructions;
            set
            {
                if (value == null) return;
                value = value.Trim();
                this._formQuestion.Instructions = (string)Validators.Check(value, "Voornaam", "MaxLength:255");
                RaisePropertyChanged("Instructions");
            }
        }

        public int Order
        {
            get => this._formQuestion.Order;
            set
            {
                this._formQuestion.Order = value;
                RaisePropertyChanged("Order");
            }
        }

        #endregion

        #region ACTIONS

        public void InsertOrUpdate(int inspectionId, UnitOfWork uow = null)
        {
            if (uow == null) uow = new UnitOfWork();

            this._formQuestion.InspectionId = inspectionId;

            if (this._formQuestion.Id != 0)
            {
                uow.FormQuestionRepository.Update(this._formQuestion);
                uow.Save();
            }
            else
            {
                uow.FormQuestionRepository.Insert(this._formQuestion);
                uow.Save();
            }

            // delete previous multiple choice
            uow.MultipleChoiceRepository
                .Get()
                .Where(mc => mc.FormQuestionId == this._formQuestion.Id)
                .ToList()
                .ForEach(mc => uow.MultipleChoiceRepository.Delete(mc));
            uow.Save();

            if (this._formQuestion.Type == "multiple_choice")
            {
                this._multplechoices.ForEach(option =>
                {
                    var newOption = new Multplechoice()
                    {
                        FormQuestionId = this._formQuestion.Id,
                        Option = option
                    };
                    uow.MultipleChoiceRepository.Insert(newOption);
                    uow.Save();
                });
            }

        }

        #endregion
    }
}
