using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.DesktopControllers;
using System;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System.Linq;

namespace Festispec.DesktopApplication.Views
{
    public sealed partial class UserCreate : Page
    {
        public UserViewModel UserViewModel { get; set; }

        DropDownItems dropDownItems { get; set; }

        public UserCreate()
        {
            this.InitializeComponent();

            this.UserViewModel = new UserViewModel();
            this.dropDownItems = new DropDownItems();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                this.UserViewModel.LoadRoles();

                this.UserViewModel.Roles.ForEach(role =>
                {
                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        dropDownItems.AddItem(role.Role, role.Role.Substring(0, 1).ToUpper() + role.Role.Substring(1, role.Role.Length - 1).ToLower());
                    });
                });
                
            });
        }

        private void SaveUser(object sender, TappedRoutedEventArgs e)
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

            if (anyHasError) return;

            try
            {
                this.UserViewModel.Insert();
                Router.GoToPage<UsersIndex>();
            }catch(Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }
    }
}
