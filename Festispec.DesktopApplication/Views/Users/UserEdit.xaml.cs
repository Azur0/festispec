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
using System.Threading;

namespace Festispec.DesktopApplication.Views
{

    public sealed partial class UserEdit : Page
    {
        public UserViewModel UserViewModel { get; set; }
        DropDownItems dropDownItems { get; set; }

        public UserEdit()
        {
            this.InitializeComponent();

            this.UserViewModel = new UserViewModel();
            this.dropDownItems = new DropDownItems();
        }

        private void LoadUser(int userId)
        {
            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                //Ophalen alle data
                UserViewModel.LoadUserByPK(userId);
                UserViewModel.LoadRoles();

                //Verwerken van data voor view.
                foreach (var role in this.UserViewModel.Roles)
                {
                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        dropDownItems.AddItem(role.Role, role.Role.Substring(0, 1).ToUpper() + role.Role.Substring(1, role.Role.Length - 1).ToLower());
                    });
                }

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.UserViewModel.UserRole = this.UserViewModel.UserRole;
                    this.Bindings.Update();
                });
            });
        }

        private void UpdateUser(object sender, TappedRoutedEventArgs e)
        {
            errorBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            var customFieldComponents = PageHelper.GetChildOfType<UserControl>(this);
            bool anyHasError = false;
            foreach (var field in customFieldComponents)
            {
                field.ForceSetValue();
                if (field.HasError == true)
                {
                    anyHasError = true;
                }
            }

            //if (anyHasError) return;

            try
            {
                this.UserViewModel.Update();
                Router.GoToPage<UsersIndex>();
            }
            catch(Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int userId = (e.Parameter as int?).Value;
            this.LoadUser(userId);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }
    }
}
