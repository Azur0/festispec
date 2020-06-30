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

    public sealed partial class CustomerEdit : Page
    {
        public CustomerViewModel CustomerViewModel { get; set; }

        private int _customerId;

        public CustomerEdit()
        {
            this.InitializeComponent();
            this.CustomerViewModel = new CustomerViewModel();
        }

        private void LoadCustomer(int userId)
        {
            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                //Ophalen alle data
                CustomerViewModel.LoadCustomerByPK(userId);

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
                });
            });
        }


        private void UpdateCustomer(object sender, TappedRoutedEventArgs e)
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
                this.CustomerViewModel.Update();
                Router.GoToPage<CustomersIndex>();
            }
            catch(Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int _customerId = (e.Parameter as int?).Value;
            this.LoadCustomer(_customerId);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

    }
}
