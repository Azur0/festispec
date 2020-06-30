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

namespace Festispec.DesktopApplication.Views.Inspections.Wizard
{
    public sealed partial class Step2 : Page
    {
        public InspectionViewModel inspectionViewModel { get; set; }
        private InspectionFormViewModel editInspectionFormViewModel { get; set; }
        private bool inEditMode = false;

        public Step2()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.inspectionViewModel = (e.Parameter as InspectionViewModel);
        }

        private void AddList(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            errorBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (listName.Text == "")
            {
                errorBox.ShowError("Naam mag niet leeg zijn");
                return;
            }

            if (inspectionViewModel.InspectionForms.Any(f => f.Name == listName.Text))
            {
                errorBox.ShowError("Alle namen moeten uniek zijn");
                return;
            }

            if (this.inEditMode)
            {
                this.editInspectionFormViewModel.Name = listName.Text;
            }
            else
            {
                this.inspectionViewModel.InspectionForms.Add(new InspectionFormViewModel()
                {
                    Name = listName.Text
                });
            }

            // reset
            this.inEditMode = false;
            listName.Text = "";
            SaveBtn.Content = "Toevoegen";
            InspectionFormsList.ItemsSource = null;
            InspectionFormsList.ItemsSource = inspectionViewModel.InspectionForms;
        }

        private void DeleteItem(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string name = ((Button)sender).Tag.ToString();
            var itemToDelete = this.inspectionViewModel.InspectionForms.Where(form => form.Name == name).First();
            this.inspectionViewModel.RemovedInspectionForms.Add(itemToDelete.Id);
            this.inspectionViewModel.InspectionForms.Remove(itemToDelete);
        }

        private void EditItem(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string name = ((Button)sender).Tag.ToString();
            var itemToEdit = this.inspectionViewModel.InspectionForms.Where(form => form.Name == name).First();
            this.inEditMode = true;
            this.editInspectionFormViewModel = itemToEdit;
            this.listName.Text = this.editInspectionFormViewModel.Name;
            SaveBtn.Content = "Update";
        }
    }
}
