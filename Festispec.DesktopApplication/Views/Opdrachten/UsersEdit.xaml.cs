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

    public sealed partial class UsersEdit : Page
    {
        public UserViewModel UserViewModel { get; set; }

        private int _userId;

        DropDownItems dropDownItems { get; set; }

        public UsersEdit()
        {
            this.InitializeComponent();

            this.UserViewModel = new UserViewModel();
            this.dropDownItems = new DropDownItems();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var roles = unitOfWork.UserRoleRepository.Get();

                foreach (var role in roles)
                {
                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        dropDownItems.AddItem(role.Role, role.Role.Substring(0, 1).ToUpper() + role.Role.Substring(1, role.Role.Length - 1).ToLower());
                    });
                }

                User dbUser = unitOfWork.UserRepository.Get(u => u.Id == this._userId).First();
                dbUser.Location = unitOfWork.LocationRepository.Get(l => l.Id == dbUser.LocationId).First();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.UserViewModel = new UserViewModel(dbUser);
                    this.UserViewModel.UserRole = this.UserViewModel.UserRole;
                    this.Bindings.Update();
                });

                unitOfWork.Dispose();
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
                // TODO: Write Update code
                this.UserViewModel.Update();
            }catch(Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._userId = (e.Parameter as int?).Value;
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

    }
}
