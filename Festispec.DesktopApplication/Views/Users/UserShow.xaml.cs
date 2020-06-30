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

    public sealed partial class UserShow : Page
    {
		// TODO: Bader wtf kut chinees. Poco van de afrika reclame is nog slimmer dan jouw.
		public User user { get; set; }

        public UserViewModel UserViewModel { get; set; }

        public UserShow()
        {
            this.InitializeComponent();
            this.UserViewModel = new UserViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int userId = (e.Parameter as int?).Value;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                UserViewModel.LoadUserByPK(userId);

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
                });
            });
           
        }

        private void EditUser(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<UserEdit>(this.user.Id);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }
    }
}
