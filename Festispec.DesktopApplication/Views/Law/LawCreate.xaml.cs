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
    public sealed partial class LawCreate : Page
    {
        public LawViewModel LawViewModel { get; set; }

        public DropDownItems dropDownItemsEvent { get; set;}



        public LawCreate()
        {
            this.InitializeComponent();

            this.LawViewModel = new LawViewModel();
            this.dropDownItemsEvent = new DropDownItems();

   
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            

        }

        private void SaveLaw(object sender, TappedRoutedEventArgs e)
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
                this.LawViewModel.Insert();
                Router.GoToPage<LawIndex>();
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


        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void FieldAbout_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

