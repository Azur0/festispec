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
    public sealed partial class CustomersIndex : Page
    {
        public CustomersIndexViewModel customersIndexViewModel { get; set; }

        public CustomersIndex()
        {
            this.InitializeComponent();

            customersIndexViewModel = new CustomersIndexViewModel();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                customersIndexViewModel.LoadAllCustomers();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.DataGrid.DataContext = this.customersIndexViewModel.customers;
                });

            });
        }

        private void OpenCustomersCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<CustomerCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.customersIndexViewModel.customers;
                return;
            }

            DataGrid.ItemsSource = this.customersIndexViewModel.customers.Where(c => {
                var searchText = SearchBox.Text.ToLower();
                var inName = c.Name.ToLower().Contains(searchText);
                var inEmail = c.Email.ToLower().Contains(searchText);
                var inAddress = c.FullAddress.ToLower().Contains(searchText);
                var inKvk = c.Kvk.ToLower().Contains(searchText);
                return inName || inEmail || inAddress || inKvk;
            }).ToList();
        }

        private void OpenCustomerViewPage(object sender, TappedRoutedEventArgs e)
        {
            var customerId = ((Button)sender).Tag;
            Router.GoToPage<CustomerShow>(customerId);
        }

        private void DeleteCustomer(object sender, TappedRoutedEventArgs e)
        {
            int customerId = (((Button)sender).Tag as int?).Value;
            this.customersIndexViewModel.DeleteCustomerByPK(customerId);
            this.DataGrid.DataContext = this.customersIndexViewModel.customers;
        }

        private void OpenCustomerEditPage(object sender, TappedRoutedEventArgs e)
        {
            int customerId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<CustomerEdit>(customerId);
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
