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
    public sealed partial class EventCreate : Page
    {
        public EventViewModel EventViewModel { get; set; }

        DropDownItems dropDownItemsCustomer { get; set; }

        public EventCreate()
        {
            this.InitializeComponent();

            this.EventViewModel = new EventViewModel();
            this.dropDownItemsCustomer = new DropDownItems();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                this.EventViewModel.LoadCustomers();

                this.EventViewModel.Customers.ForEach(customer =>
                {
                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        dropDownItemsCustomer.AddItem(customer.Id, customer.Name.Substring(0, 1).ToUpper() + customer.Name.Substring(1, customer.Name.Length - 1).ToLower());
                    });
                });
               
            });
        }

        private void SaveEvent(object sender, TappedRoutedEventArgs e)
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
                this.EventViewModel.Insert();
                Router.GoToPage<EventsIndex>();
            }
            catch (Exception databaseError)
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

