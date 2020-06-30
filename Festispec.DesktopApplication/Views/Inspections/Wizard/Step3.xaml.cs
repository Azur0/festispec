using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.DesktopControllers;
using System;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using Festispec.DesktopApplication.Views.Components;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;

namespace Festispec.DesktopApplication.Views.Inspections.Wizard
{
    public sealed partial class Step3 : Page
    {
        public InspectionViewModel inspectionViewModel { get; set; }

        public Step3()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.inspectionViewModel = (e.Parameter as InspectionViewModel);
            this.inspectionViewModel.LoadTemplates();
        }

        private void combobox_Changed(object sender, SelectionChangedEventArgs e)
        {
            questionBuilder.IsEnabled = true;
            templates.IsEnabled = true;
            listViewQuestions.ItemsSource = inspectionViewModel.SelectedInspectionForm.FormQuestions;
        }

        public Action<FormQuestionViewModel> OnSave => formQuestionViewModel => 
        {
            inspectionViewModel.SelectedInspectionForm.FormQuestions.Add(formQuestionViewModel);
            listViewQuestions.ItemsSource = inspectionViewModel.SelectedInspectionForm.FormQuestions;
        };

        private void ExpandGroupbox_onDeleteEvent(object sender, FormQuestionViewModel formQuestionViewModel)
        {
            inspectionViewModel.SelectedInspectionForm.FormQuestions.Remove(formQuestionViewModel);
            inspectionViewModel.SelectedInspectionForm.RemovedQuestions.Add(formQuestionViewModel.Id);
            listViewQuestions.ItemsSource = inspectionViewModel.SelectedInspectionForm.FormQuestions;
        }

        private void ExpandGroupbox_onEditEvent(object sender, FormQuestionViewModel formQuestionViewModel)
        {
            questionBuilder.EditQuestion(formQuestionViewModel);
        }

        private void StackPanel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var templateId = Int32.Parse((sender as StackPanel).Tag.ToString());
            var template = inspectionViewModel.Templates.Where(t => t.Id == templateId).First();
            template.Questions.ForEach(questionVM =>
            {
                var newModel = new FormQuestion() {
                    Question = questionVM.GetModel().Question,
                    Instructions = questionVM.GetModel().Instructions,
                    Type = questionVM.GetModel().Type,
                    Multplechoice = questionVM.GetModel().Multplechoice.Select(x => new Multplechoice() { Option = x.Option }).ToList()
                };

                var a = new FormQuestionViewModel(newModel);

                inspectionViewModel.SelectedInspectionForm.FormQuestions.Add(a);
            });
        }
    }

}
