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

    public sealed partial class LawShow : Page
    {
        public LawViewModel LawViewModel {get; set;}


        public LawShow()
        {
            this.LawViewModel = new LawViewModel();
           
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int lawId = (e.Parameter as int?).Value;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                LawViewModel.LoadLawByPK(lawId);

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.Bindings.Update();
                });
            });

           

        }

        private void EditLaw(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<LawEdit>(this.LawViewModel.Id);
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
