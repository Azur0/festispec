using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.DesktopControllers;
using System;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System.Linq;
using System.Collections.Generic;
using Festispec.DesktopApplication.Views.Inspections.Wizard;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml.Navigation;

namespace Festispec.DesktopApplication.Views
{
    public sealed partial class InspectionEdit : Page
    {
        public InspectionViewModel inspectionViewModel { get; set; }
        private List<Type> wizardPages = new List<Type>();
        private int currentPage { get; set; }

        public InspectionEdit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int inspectionId = (e.Parameter as int?).Value;

            this.inspectionViewModel = new InspectionViewModel();

            this.inspectionViewModel.LoadInspectionByPK(inspectionId);

            wizardPages.Add(typeof(Step1));
            wizardPages.Add(typeof(Step2));
            wizardPages.Add(typeof(Step3));
            wizardPages.Add(typeof(Step4));

            this.goToStep(0);
        }

        private void goToStep(int index)
        {
            currentPage = index;

            if (index == 0)
            {
                prevBtn.Content = "Annuleren";
                nextBtn.Content = "Volgende";
            }
            else if (index == (wizardPages.Count - 1))
            {
                prevBtn.Content = "Vorige";
                nextBtn.Content = "Opslaan";
            }
            else
            {
                prevBtn.Content = "Vorige";
                nextBtn.Content = "Volgende";
            }

            int stepCount = 0;
            foreach (var step in steps.Children)
            {
				StackPanel stackpanel = step as StackPanel;
				if(stackpanel != null)
				{
					( step as StackPanel ).Background = new SolidColorBrush(Color.FromArgb(99, 179, 237, 255));
					if(stepCount == index)
					{
						( step as StackPanel ).Background = new SolidColorBrush(Color.FromArgb(213, 63, 140, 255));
					}
					stepCount++;
				}
            }

            wizardFrame.Navigate(wizardPages[index], inspectionViewModel);
        }

        private void prevBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.currentPage == 0)
            {
                Router.GoBack();
                return;
            }
            this.goToStep(this.currentPage - 1);
        }

        private void nextBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.currentPage == (this.wizardPages.Count - 1))
            {
                if (inspectionViewModel.EventId == 0)
                {
                    MessageBox.Show("Inspectie moet een event hebben");
                    return;
                }

                if ((inspectionViewModel.Name == null) && (inspectionViewModel.Name == ""))
                {
                    MessageBox.Show("Inspectie moet een naam hebben");
                    return;
                }

                if (inspectionViewModel.InspectionForms.Count == 0)
                {
                    MessageBox.Show("Inspectie moet vragenlijsten bevatten");
                    return;
                }

                if (inspectionViewModel.InspectionForms.Any(form => form.FormQuestions.Count == 0))
                {
                    MessageBox.Show("Alle vragenlijsten moeten vragen bevatten");
                    return;
                }

                inspectionViewModel.Update();
                Router.GoToPage<InspectionsIndex>();
                return;
            }
            this.goToStep(this.currentPage + 1);
        }
    }
}
