using System;
using System.Collections.Generic;
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
using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using System.Diagnostics;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportsIndex : Page
    {
        public EventOverviewViewModel EventOverviewViewModel { get; set; }

        public ReportsIndex()
        {
            this.InitializeComponent();

            EventOverviewViewModel = new EventOverviewViewModel();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                EventOverviewViewModel.LoadAllUsers();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.DataGrid.DataContext = this.EventOverviewViewModel.Events;
                });

            });
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.EventOverviewViewModel.Events;
                return;
            }

            DataGrid.ItemsSource = this.EventOverviewViewModel.Events.Where(ev =>
            {
                var searchText = SearchBox.Text.ToLower();
                var inName = ev.Name.ToLower().Contains(searchText);
                var inPaymentStatus = ev.PaymentStatus.ToLower().Contains(searchText);
                var inStartDate = ev.StartDate.ToString().Contains(searchText);
                var inEnddate = ev.EndDate.ToString().Contains(searchText);
                return inName || inPaymentStatus || inStartDate || inEnddate;
            }).ToList();
        }

        private async void OpenReport(object sender, RoutedEventArgs e)
        {
            var reportId = (sender as Button).Tag.ToString();
            string command = "http://vestispec.azurewebsites.net/Report/Index/" + reportId;
            await Launcher.LaunchUriAsync(new Uri(command));
        }
    }
}
