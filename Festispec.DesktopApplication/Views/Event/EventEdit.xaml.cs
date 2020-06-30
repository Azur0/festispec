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
    public sealed partial class EventEdit : Page
    {
        public EventViewModel EventViewModel { get; set; }
        DropDownItems dropDownItemsCustomer { get; set; }
        DropDownItems dropDownItemsPaymentStatus { get; set; }

        public EventEdit()
        {
            this.InitializeComponent();
            this.EventViewModel = new EventViewModel();
            this.dropDownItemsCustomer = new DropDownItems();
            this.dropDownItemsPaymentStatus = new DropDownItems();
        }

        private void LoadEvent(int eventId)
        {
            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                // Ophalen van huidige event
                EventViewModel.LoadEventByPK(eventId);
                EventViewModel.LoadCustomers();
                EventViewModel.LoadPaymentStatus();

                // fill dropdown with customers
                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.EventViewModel.Customers.ForEach(customer =>
                    {
                        dropDownItemsCustomer.AddItem(customer.Id, customer.Name.Substring(0, 1).ToUpper() + customer.Name.Substring(1, customer.Name.Length - 1).ToLower());
                    });

                    // fill dropdown with payment statusses
                    this.EventViewModel.PaymentStatusList.ForEach(PaymentStatus =>
                    {
                        dropDownItemsPaymentStatus.AddItem(PaymentStatus, PaymentStatus.Substring(0, 1).ToUpper() + PaymentStatus.Substring(1, PaymentStatus.Length - 1).ToLower());
                    });

                    this.EventViewModel.PaymentStatus = this.EventViewModel.PaymentStatus;
                    this.EventViewModel.CustomerId = this.EventViewModel.CustomerId;
                    this.Bindings.Update();
                });
            });
        }


        private void UpdateEvent(object sender, TappedRoutedEventArgs e)
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
                this.EventViewModel.Update();
                Router.GoToPage<EventsIndex>();
            }
            catch (Exception databaseError)
            {
                errorBox.ShowError(databaseError.InnerException.Message);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int _eventId = (e.Parameter as int?).Value;
            this.LoadEvent(_eventId);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

    }
}

