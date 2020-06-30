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

    public sealed partial class UsersShow : Page
    {
        public User user { get; set; }

        public UsersShow()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var userId = e.Parameter as int?;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                // TODO: Get user by ID
                var dbUser = unitOfWork.UserRepository.Get(u => u.Id == userId).First();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.user = dbUser;
                    this.Bindings.Update();
                });
            });
           
        }

        private void EditUser(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<UsersEdit>(this.user.Id);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }
    }
}
