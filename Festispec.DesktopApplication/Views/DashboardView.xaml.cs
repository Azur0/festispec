using Festispec.DesktopApplication.ViewModels;
using Newtonsoft.Json;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DashboardView : Page
	{
		private DashboardViewModel dashboardViewModel;

		public DashboardView()
		{
			dashboardViewModel = new DashboardViewModel();

			this.InitializeComponent();

            //var locations = dashboardViewModel.GetLocations();

            webView.Source = new Uri("ms-appx-web:///Views/map.html");
            webViewCalendar.Source = new Uri("ms-appx-web:///Views/calendar.html");
        }

        private void webView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var locations = dashboardViewModel.GetLocations();

            string script = String.Empty;

            foreach(var location in locations)
            {
                script += $"addToLocation('{location.Item1}', '{location.Item2}', {location.Item3.ToString().Replace(',', '.')}, {location.Item4.ToString().Replace(',', '.')});";
            }

            script += @"setMarkers();";

            string[] args = { script };
            string foo = webView.InvokeScriptAsync("eval", args).ToString();
        }

        private void webViewCalendar_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var avlibilty = dashboardViewModel.GetAvailability();
            var data = JsonConvert.SerializeObject(avlibilty);
            string[] args = { $"var data = JSON.parse('{data}'); loadCalendar();" };
            webViewCalendar.InvokeScriptAsync("eval", args).ToString();
        }
    }
}
