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
    public sealed partial class UsersView : Page
    {
        public List<TextFilterDescriptor> filters { get; set; }
        public TextFilterDescriptor nameFilter { get; set; }
        public TextFilterDescriptor emailFilter { get; set; }
        public TextFilterDescriptor addressFilter { get; set; }
        public TextFilterDescriptor roleFilter { get; set; }

        public UsersView()
        {
            this.InitializeComponent();

            SetFilter();

            // Test data

            var list = Enumerable.Range(1, 20);
            this.DataGrid.ItemsSource = list.Select(v => new { Name = "name " + v, Email = "email " + v, Address = "adres " + v, Role = "user",  }).ToList();
            
        }

        private void OpenUsersCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<UsersCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                foreach(var filter in filters)
                {
                    DataGrid.FilterDescriptors.Remove(filter);
                }
            }
            else
            {
                foreach(var filter in filters)
                {
                    if (!DataGrid.FilterDescriptors.Contains(filter))
                        DataGrid.FilterDescriptors.Add(filter);
                }
            }

            foreach(var filter in filters)
            {
                filter.Value = ((TextBox)sender).Text;
            }
        }

        private void PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void SetFilter()
        {
            nameFilter = new TextFilterDescriptor() { PropertyName = "Naam", IsCaseSensitive = false, Operator = TextOperator.Contains};
            //emailFilter = new TextFilterDescriptor() { PropertyName = "Email", IsCaseSensitive = false, Operator = TextOperator.Contains };
            //addressFilter = new TextFilterDescriptor() { PropertyName = "Adres", IsCaseSensitive = false, Operator = TextOperator.Contains };
            //roleFilter = new TextFilterDescriptor() { PropertyName = "Rol", IsCaseSensitive = false, Operator = TextOperator.Contains };

            filters = new List<TextFilterDescriptor>() {
                nameFilter,
                //emailFilter,
                //addressFilter,
                //roleFilter
            };
        }
    }
}
