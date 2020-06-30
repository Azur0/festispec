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
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Festispec.DesktopApplication.Views.Inspections.Wizard
{
    public sealed partial class Step4 : Page
    {
        public InspectionViewModel inspectionViewModel { get; set; }
        private int draggedUserID { get; set; }

        public Step4()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.inspectionViewModel = (e.Parameter as InspectionViewModel);
            inspectionViewModel.LoadAvalibleUsers();
        }

        private void ListView_DragOver(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            //(sender as UserControl).Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));
            e.AcceptedOperation = DataPackageOperation.Link;
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            var form = (sender as StackPanel).DataContext as InspectionFormViewModel;
            this.inspectionViewModel.AssignUserToForm(form, this.draggedUserID);
        }

        private void StackPanel_DragStarting(UIElement sender, DragStartingEventArgs args)
        {

            this.draggedUserID = Int32.Parse((sender as Grid).Tag.ToString());
        }

        private void UserSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var search = UserSearch.Text;
            UserList.ItemsSource = inspectionViewModel.AvalibleUsers.Where(x => x.UserViewModel.FullName.ToLower().Contains(search));
        }

        private void RemoveAsingee(object sender, RoutedEventArgs e)
        {
            var userId = Int32.Parse((sender as Button).Tag.ToString());
            var form = ((sender as Button).Parent as StackPanel).Tag as InspectionFormViewModel;
            form.RemovedAsingees.Add(userId);
            form.Assignees.Remove(form.Assignees.Where(u => u.Id == userId).First());
        }
    }
}
