using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.Views;
using FestiSpec.SharedCode.Repositories;
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
    public sealed partial class EventsIndex : Page
    {
        public EventOverviewViewModel EventOverviewViewModel { get; set; }

        public EventsIndex()
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

        private void OpenEventCreatePage(object sender, TappedRoutedEventArgs e)
        {
         Router.GoToPage<EventCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
            DataGrid.ItemsSource = this.EventOverviewViewModel.Events;
            return;
            }

            DataGrid.ItemsSource = this.EventOverviewViewModel.Events.Where(ev => {
            var searchText = SearchBox.Text.ToLower();
            var inName = ev.Name.ToLower().Contains(searchText);
            var inPaymentStatus = ev.PaymentStatus.ToLower().Contains(searchText);
            var inStartDate = ev.StartDate.ToString().Contains(searchText);
            var inEnddate = ev.EndDate.ToString().Contains(searchText);
            return inName || inPaymentStatus || inStartDate || inEnddate;
           }).ToList();
    }

    private void OpenEventViewPage(object sender, TappedRoutedEventArgs e)
    {
        var eventId = ((Button)sender).Tag;
        Router.GoToPage<EventShow>(eventId);
    }

    private void OpenEventEditPage(object sender, TappedRoutedEventArgs e)
    {
        int eventId = (((Button)sender).Tag as int?).Value;
        Router.GoToPage<EventEdit>(eventId);
    }

    private void DeleteEvent(object sender, TappedRoutedEventArgs e)
    {
        int userId = (((Button)sender).Tag as int?).Value;
        this.EventOverviewViewModel.DeleteEventByPK(userId);
        this.DataGrid.DataContext = this.EventOverviewViewModel.Events;
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
}

}


