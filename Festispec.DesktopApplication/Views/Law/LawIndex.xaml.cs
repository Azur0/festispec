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
    public sealed partial class LawIndex : Page
    {
        public LawIndexViewModel LawIndexViewModel { get; set; }

        public LawIndex()
        {
            this.InitializeComponent();

            LawIndexViewModel = new LawIndexViewModel();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                LawIndexViewModel.LoadAllLaws();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.DataGrid.DataContext = this.LawIndexViewModel.laws;
                });

            });
        }

        private void OpenlawCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<LawCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.LawIndexViewModel.laws;
                return;
            }

            DataGrid.ItemsSource = this.LawIndexViewModel.laws.Where(c => {
                var searchText = SearchBox.Text.ToLower();
                var inName = c.Name.ToLower().Contains(searchText);
                var inCity = c.City.ToString().Contains(searchText);
                return inName || inCity;
            }).ToList();
        }

        private void OpenLawViewPage(object sender, TappedRoutedEventArgs e)
        {
            var lawId = ((Button)sender).Tag;
            Router.GoToPage<LawShow>(lawId);
        }

        private void DeleteLaw(object sender, TappedRoutedEventArgs e)
        {
            int lawId = (((Button)sender).Tag as int?).Value;
            this.LawIndexViewModel.DeleteLawByPK(lawId);
            this.DataGrid.DataContext = this.LawIndexViewModel.laws;
        }

        private void OpenLawEditPage(object sender, TappedRoutedEventArgs e)
        {
            int lawId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<LawEdit>(lawId);
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
