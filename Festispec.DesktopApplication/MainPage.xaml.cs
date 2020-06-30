using Festispec.DesktopApplication.ViewModels;
using CommonServiceLocator;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight.Ioc;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.Views;
using Festispec.DesktopApplication.Views.Inspections;
using Festispec.DesktopApplication.DesktopControllers;

namespace Festispec.DesktopApplication
{
	public sealed partial class MainPage : Page
	{
		public MainViewModel MainViewModel { get; set; }
        public User user { get; set; }

		public MainPage()
		{
			this.InitializeComponent();
			this.MainViewModel = new MainViewModel();
            this.user = Router.User;
            Router.SetPage(ref ContentFrame);
            Router.GoToPage<DashboardView>();

            
        }

		private void OpenView(object sender, TappedRoutedEventArgs e)
		{
			switch (((NavigationViewItem)sender).Name)
			{
				case "Dashboard":
					Router.GoToPage<DashboardView>();
					break;
				case "Users":
                    if (NetworkController.HasInternet() == false) return;
                    Router.GoToPage<UsersIndex>();
					break;
				case "Customers":
                    if (NetworkController.HasInternet() == false) return;
                    Router.GoToPage<CustomersIndex>();
					break;
                case "Assignments":
                    if (NetworkController.HasInternet() == false) return;
                    Router.GoToPage<EventsIndex>();
                    break;
                case "Inspections":
                    Router.GoToPage<InspectionsIndex>();
                    break;
                case "Templates":
                    if (NetworkController.HasInternet() == false) return;
                    Router.GoToPage<TemplatesIndex>();
                    break;
                case "Reports":
                    Router.GoToPage<ReportsIndex>();
                    break;
                case "Legislation":
                    if (NetworkController.HasInternet() == false) return;
                    Router.GoToPage<LawIndex>();
                    break;
            }
		}

		private void PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
		}

		private void PointerExited(object sender, PointerRoutedEventArgs e)
		{
			Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
		}

        private void Logout(object sender, TappedRoutedEventArgs e)
        {
            Router.User = null;
            Router.RootGoTo<LoginView>();
        }
    }
}
