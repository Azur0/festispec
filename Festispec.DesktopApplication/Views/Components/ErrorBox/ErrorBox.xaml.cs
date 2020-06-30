using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
    public partial class ErrorBox : UserControl
    {
        public ErrorBox()
        {
            this.InitializeComponent();
        }

        public void ShowError(string message)
        {
            this.Visibility = Visibility.Visible;
            this.lblError.Text = message;
		}

        private void Close(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
