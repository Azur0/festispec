using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
	public sealed partial class QuestionBuilder : UserControl
	{
        
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public List<string> MultpleChoices { get; set; }
        public string Instructions { get; set; }

        DropDownItems dropDownItems { get; set; }

        public FormQuestionViewModel FormQuestionViewModel { get; set; }
        public Action<FormQuestionViewModel> onSaveEvent { get; set; }
		public int ComponenWidth { get; set; }

        private FormQuestionViewModel editFormQuestionViewModel { get; set; }
        private bool InEditMode = false;

        public QuestionBuilder()
		{
			this.InitializeComponent();
            MultpleChoices = new List<string>();

            FormQuestionViewModel = new FormQuestionViewModel();
			dropDownItems = new DropDownItems();

			//TODO: load from database
			dropDownItems.AddItem("image","Afbeelding");
			dropDownItems.AddItem("multiple_choice","Meerkeuze");
			dropDownItems.AddItem("open","Open Vraag");
		}

        private void onSaveClick(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Validate
            FieldQuestion.HideError();
            FieldQuestionType.HideError();

            if ( (this.Question == null) || this.Question == "")
            {
                FieldQuestion.ShowError("Vraag mag niet leeg zijn");
                return;
            }

            if (this.QuestionType == null)
            {
                FieldQuestionType.ShowError("Type mag niet leeg zijn");
                return;
            }

            if (this.QuestionType == "multiple_choice")
            {
                if (this.MultpleChoices.Count <= 1)
                {
                    FillableList.ShowError("Minimaal 2 antwoorden toevoegen");
                    return;
                }
            }

            // emit the save event
            if (InEditMode)
            {
                var options = new List<string>();
                
                this.editFormQuestionViewModel.Question = this.Question;
                this.editFormQuestionViewModel.QuestionType = this.QuestionType;
                if (this.QuestionType == "multiple_choice")
                {
                    this.MultpleChoices.ForEach(x => options.Add(x.ToString()));
                }
                this.editFormQuestionViewModel.MultpleChoices = options;
                this.editFormQuestionViewModel.Instructions = this.Instructions;
            }
            else
            {
                var options = new List<string>();
                this.MultpleChoices.ForEach(x => options.Add(x.ToString()));
                onSaveEvent(new FormQuestionViewModel()
                {
                    Question = this.Question,
                    QuestionType = this.QuestionType,
                    MultpleChoices = options,
                    Instructions = this.Instructions
                });
            }

            // reset the builder
            this.Question = null;
            this.QuestionType = null;
            this.Instructions = null;
            this.MultpleChoices = new List<string>();
            this.InEditMode = false;
            SaveBtn.Content = "Toevoegen";
            FieldQuestionType.Reset();
            FillableList.Reset();
            Bindings.Update();
        }

        public Action<Tuple<object, string>> typeSelectionChange => selectedItem => {
            if (selectedItem == null) {
                this.QuestionType = null;
            }
            else
            {
                this.QuestionType = selectedItem.Item1.ToString();
            }

            if (this.QuestionType == "multiple_choice")
            {
                FillableList.Visibility = Visibility.Visible;
            }
            else
            {
                FillableList.Visibility = Visibility.Collapsed;
            }
        };

        public void EditQuestion(FormQuestionViewModel formQuestionViewModel)
        {
            this.InEditMode = true;
            this.editFormQuestionViewModel = formQuestionViewModel;
            this.Question = formQuestionViewModel.Question;
            FieldQuestionType.SelectedValue = this.dropDownItems.GetOption(formQuestionViewModel.QuestionType);
            this.QuestionType = formQuestionViewModel.QuestionType;
            this.MultpleChoices = formQuestionViewModel.MultpleChoices;
            this.Instructions = formQuestionViewModel.Instructions;
            FillableList.SetOptions(this.MultpleChoices);
            SaveBtn.Content = "Update";
            Bindings.Update();
        }
    }
}
