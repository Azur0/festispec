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

namespace Festispec.DesktopApplication.Views
{

    public sealed partial class EventShow : Page
    {
        //public EventOverviewViewModel EventOverviewViewModel { get; set; }

        public EventViewModel EventViewModel { get; set; }

        public EventShow()
        {
            //this.EventOverviewViewModel = new EventOverviewViewModel();
            
            this.EventViewModel = new EventViewModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int eventId = (e.Parameter as int?).Value;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                EventViewModel.LoadEventByPK(eventId);
                EventViewModel.LoadQuoations();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
                });
            });

        }

        private void CreateInspection(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<InspectionCreate>(this.EventViewModel.Id);
        }

        private void CreateQuotation(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<QuotationCreate>(this.EventViewModel);
        }

        private void EditEvent(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<EventEdit>(this.EventViewModel.Id);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }
        
        private void GroupWrapper_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void ShowQuotation(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var quoteId = Int32.Parse((sender as Button).Tag.ToString());
            Router.GoToPage<QuotationShow>(quoteId);
        }
    }
}
