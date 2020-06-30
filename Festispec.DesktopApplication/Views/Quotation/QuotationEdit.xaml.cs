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
    public sealed partial class QuotationEdit : Page
    {
        public QuotationViewModel QuotationViewModel { get; set; }
        DropDownItems dropDownItemsQuotation { get; set; }
        

        public QuotationEdit()
        {
            this.InitializeComponent();
            this.QuotationViewModel = new QuotationViewModel();
            this.dropDownItemsQuotation = new DropDownItems();
           
        }

        private void LoadEvent(int quotationId)
        {
            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                // Ophalen van huidige event
                QuotationViewModel.LoadQuotationByPK(quotationId);

                QuotationViewModel.LoadStatus();

                // fill dropdown with customers
                PageHelper.MainUI(Dispatcher, () =>
                {
                    // fill dropdown with payment statusses
                    this.QuotationViewModel.QuotationStatusList.ForEach(QuotationStatus =>
                    {
                        dropDownItemsQuotation.AddItem(QuotationStatus, QuotationStatus.Substring(0, 1).ToUpper() + QuotationStatus.Substring(1, QuotationStatus.Length - 1).ToLower());
                    });
                    this.QuotationViewModel.QuotationStatus = this.QuotationViewModel.QuotationStatus;
                    this.Bindings.Update();

                });
            });
        }


        private void UpdateQuotation(object sender, TappedRoutedEventArgs e)
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
                this.QuotationViewModel.Update();
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

