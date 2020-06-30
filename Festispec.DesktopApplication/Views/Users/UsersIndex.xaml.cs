using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsersIndex : Page
    {
        public UsersIndexViewModel usersIndexViewModel { get; set; }

        public UsersIndex()
        {
            this.InitializeComponent();

            usersIndexViewModel = new UsersIndexViewModel();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                usersIndexViewModel.LoadAllUsers();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.DataGrid.DataContext = this.usersIndexViewModel.Users;
                });

            });
        }

        private void OpenUsersCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<UserCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.usersIndexViewModel.Users;
                return;
            }

            DataGrid.ItemsSource = this.usersIndexViewModel.Users.Where(u => {
                var searchText = SearchBox.Text.ToLower();
                var inName = u.FullName.ToLower().Contains(searchText);
                var inEmail = u.Email.ToLower().Contains(searchText);
                var inAddress = u.FullAddress.ToLower().Contains(searchText);
                var inRole = u.UserRole.ToLower().Contains(searchText);
                return inName || inEmail || inAddress || inRole;
            }).ToList();
        }

        private void OpenUsersViewPage(object sender, TappedRoutedEventArgs e)
        {
            var userId = ((Button)sender).Tag;
            Router.GoToPage<UserShow>(userId);
        }

        private void OpenUsersEditPage(object sender, TappedRoutedEventArgs e)
        {
            int userId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<UserEdit>(userId);
        }

        private void DeleteUser(object sender, TappedRoutedEventArgs e)
        {
            int userId = (((Button)sender).Tag as int?).Value;
            this.usersIndexViewModel.DeleteUserByPK(userId);
            this.DataGrid.DataContext = this.usersIndexViewModel.Users;
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
