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

    public sealed partial class QuotationShow : Page
    {
        public QuotationViewModel QuotationViewModel {get; set;}

        DropDownItems dropDownItemsStatus { get; set; }
        public EventViewModel EventViewModel { get; set; }

        public QuotationShow()
        {
            this.QuotationViewModel = new QuotationViewModel();
            this.dropDownItemsStatus = new DropDownItems();

            //PageHelper.DoAsync(overlay, Dispatcher, () =>
            //{
            //    this.QuotationViewModel.LoadCustomers();

            //    this.QuotationViewModel.Customers.ForEach(customer =>
            //    {
            //        PageHelper.MainUI(Dispatcher, () =>
            //        {
            //            dropDownItemsstatus.AddItem(customer.Name, customer.Name.Substring(0, 1).ToUpper() + customer.Name.Substring(1, customer.Name.Length - 1).ToLower());
            //        });
            //    });

            //});
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int quotationId = (e.Parameter as int?).Value;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                QuotationViewModel.LoadQuotationByPK(quotationId);
               

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
                });
            });

           

        }

        private void EditQuotation(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<QuotationEdit>(this.QuotationViewModel.Id);
        }

        private void CancelClick(object sender, TappedRoutedEventArgs e)
        {
            Router.GoBack();
        }

        private void GroupWrapper_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
