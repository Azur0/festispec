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
    public sealed partial class CustomerShow : Page
    {
		public CustomerViewModel customerViewModel { get; set; }

        public CustomerShow()
        {
			this.customerViewModel = new CustomerViewModel();
   
			this.InitializeComponent();

			webView.Source = new Uri("ms-appx-web:///Views/map.html");
		}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var customerId = ( e.Parameter as int? ).Value;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();

				customerViewModel.LoadCustomerByPK(customerId);

				PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
				});
            });
           
        }

		private void webView_LoadCompleted(object sender, NavigationEventArgs e)
		{
			var locations = customerViewModel.GetLocations();

			string script = String.Empty;

			foreach(var location in locations)
			{
				script += $"addToLocation('{location.Item1}', '{location.Item2}', {location.Item3.ToString().Replace(',', '.')}, {location.Item4.ToString().Replace(',', '.')});";
			}

			script += @"setMarkers();";

			string[] args = { script };
			string foo = webView.InvokeScriptAsync("eval", args).ToString();
		}

		private void EditCustomer(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<CustomerEdit>(this.customerViewModel.Id);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

        private void OpenEventShow(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var eventId = Int32.Parse((sender as Button).Tag.ToString());
            Router.GoToPage<EventShow>(eventId);
        }

		private void CreateEvent(object sender, TappedRoutedEventArgs e)
		{
			Router.GoToPage<EventCreate>(this.customerViewModel.Id);
		}
	}
}
