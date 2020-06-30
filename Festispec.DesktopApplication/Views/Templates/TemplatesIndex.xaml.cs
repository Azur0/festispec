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
    public sealed partial class TemplatesIndex : Page
    {
        public TemplatesIndexViewModel templatesIndexViewModel { get; set; }

        public TemplatesIndex()
        {
            this.InitializeComponent();

            templatesIndexViewModel = new TemplatesIndexViewModel();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                templatesIndexViewModel.LoadAllTemplates();

                PageHelper.MainUI(Dispatcher, () =>
                {
                    this.DataGrid.DataContext = this.templatesIndexViewModel.Templates;
                });

            });
        }

        private void OpenTemplateCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<TemplateCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                DataGrid.ItemsSource = this.templatesIndexViewModel.Templates;
                return;
            }

            DataGrid.ItemsSource = this.templatesIndexViewModel.Templates.Where(form =>
            {
                var searchText = SearchBox.Text.ToLower();
                var inName = form.Name.ToLower().Contains(searchText);
                return inName;
            }).ToList();
        }

		private void OpenTemplateShowPage(object sender, TappedRoutedEventArgs e)
		{
			int templateId = ( ( (Button)sender ).Tag as int? ).Value;
			Router.GoToPage<TemplateShow>(templateId);
		}

		private void OpenTemplateEditPage(object sender, TappedRoutedEventArgs e)
        {
            int templateId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<TemplateEdit>(templateId);
        }

        private void DeleteTemplate(object sender, TappedRoutedEventArgs e)
        {
            int userId = (((Button)sender).Tag as int?).Value;
            this.templatesIndexViewModel.DeleteUserByPK(userId);
            this.DataGrid.DataContext = this.templatesIndexViewModel.Templates;
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
