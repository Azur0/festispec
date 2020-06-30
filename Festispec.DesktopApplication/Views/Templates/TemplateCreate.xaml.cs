using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TemplateCreate : Page
	{
		public InspectionFormViewModel inspectionFormViewModel;

		public TemplateCreate()
		{
			this.InitializeComponent();
			inspectionFormViewModel = new InspectionFormViewModel();
            ListViewQuestions.ItemsSource = inspectionFormViewModel.FormQuestions;
        }

        private void SaveTemplate(object sender, TappedRoutedEventArgs e)
        {
            errorBox.Visibility = Visibility.Collapsed;

            FieldName.ForceSetValue();
            if (FieldName.HasError) return;

            if (inspectionFormViewModel.FormQuestions.Count == 0)
            {
                errorBox.ShowError("Template moet minimaal 1 vraag bevatten.");
                return;
            }

            try
            {
                this.inspectionFormViewModel.Insert();
                Router.GoToPage<TemplatesIndex>();
            }
            catch (Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

        public Action<FormQuestionViewModel> OnSave => formQuestionViewModel => {
            inspectionFormViewModel.FormQuestions.Add(formQuestionViewModel);
        };

        private void DeleteQuestion(object sender, FormQuestionViewModel formQuestionVM)
        {
            inspectionFormViewModel.FormQuestions.Remove(formQuestionVM);
        }

        private void onEditEvent(object sender, FormQuestionViewModel formQuestionViewModel)
        {
            questionBuilder.EditQuestion(formQuestionViewModel);
        }
    }

}
