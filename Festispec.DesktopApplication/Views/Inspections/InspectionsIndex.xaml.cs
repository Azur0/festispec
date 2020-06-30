using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using Newtonsoft.Json;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Data.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InspectionsIndex : Page
    {
        public InspectionsIndexViewModel inspectionsIndexViewModel { get; set; }

        public InspectionsIndex()
        {
            this.InitializeComponent();

            inspectionsIndexViewModel = new InspectionsIndexViewModel();

            if (NetworkController.HasInternet())
            {
                PageHelper.DoAsync(overlay, Dispatcher, () =>
                {
                    inspectionsIndexViewModel.LoadAllInspections();
                    inspectionsIndexViewModel.inspections.ForEach(i => i.LoadInspectionByPK(i.Id));
                    this.saveOffline();

                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        this.DataGrid.DataContext = this.inspectionsIndexViewModel.inspections;
                    });

                });
            }
            else
            {
                this.loadFromFile();
            }
        }

        private async void loadFromFile()
        {
            var offlineController  = new OfflineController();
            var data = await offlineController.GetOfflineData<InspectionsIndexViewModel>("Inspections");
            this.inspectionsIndexViewModel = data.value;

            PageHelper.MainUI(Dispatcher, () =>
            {
                this.DataGrid.DataContext = this.inspectionsIndexViewModel.inspections;
            });
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.inspectionsIndexViewModel.inspections;
                return;
            }

            DataGrid.ItemsSource = this.inspectionsIndexViewModel.inspections.Where(inspection => {
                var searchText = SearchBox.Text.ToLower();
                var inName = inspection.Name.ToLower().Contains(searchText);
                return inName;
            }).ToList();
        }

        private void OpenInspectionCreatePage(object sender, TappedRoutedEventArgs e)
        {
            if (NetworkController.HasInternet() == false) return;
            Router.GoToPage<InspectionCreate>();
        }

        private void OpenInspectionViewPage(object sender, TappedRoutedEventArgs e)
        {
            string inspectionName = (((Button)sender).Tag as string);
            var inspectionVM = this.inspectionsIndexViewModel.inspections.Where(inspection => inspection.Name == inspectionName).First();
            Router.GoToPage<InspectionShow>(inspectionVM);
        }

        private void OpenInspectionEditPage(object sender, TappedRoutedEventArgs e)
        {
            if (NetworkController.HasInternet() == false) return;
            int inspectionId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<InspectionEdit>(inspectionId);
        }

        private void DeleteInspection(object sender, TappedRoutedEventArgs e)
        {
            if (NetworkController.HasInternet() == false) return;
            int inspectionId = (((Button)sender).Tag as int?).Value;
            this.inspectionsIndexViewModel.DeleteInspectionByPK(inspectionId);
            this.DataGrid.DataContext = this.inspectionsIndexViewModel.inspections;
        }

        #region HOVER
        private void PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
        #endregion

        private async void saveOffline()
        {
            var offlineController = new OfflineController();
            var inspectionVM = this.inspectionsIndexViewModel;
            await offlineController.SaveToFile<InspectionsIndexViewModel>("Inspections", inspectionVM);
        }
    }

}
