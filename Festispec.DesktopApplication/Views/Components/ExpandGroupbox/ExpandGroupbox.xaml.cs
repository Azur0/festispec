using Festispec.DesktopApplication.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
    public partial class ExpandGroupbox : UserControl
    {
        public FormQuestionViewModel FormQuestionViewModel { get; set; }

        public event EventHandler<FormQuestionViewModel> onDeleteEvent;
        public event EventHandler<FormQuestionViewModel> onEditEvent;


        public ExpandGroupbox()
        {
            this.InitializeComponent();
        }

        private void ToggleClick(object sender, TappedRoutedEventArgs e)
        {
            if (contentPanel.Visibility == Visibility.Visible)
            {
                contentPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                contentPanel.Visibility = Visibility.Visible;
            }
        }

        private void OnEditClick(object sender, TappedRoutedEventArgs e)
        {
            onEditEvent.Invoke(this, this.FormQuestionViewModel);
        }

        private void OnDeleteClick(object sender, TappedRoutedEventArgs e)
        {
            onDeleteEvent.Invoke(this, this.FormQuestionViewModel);
        }

        private void root_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var context = args.NewValue;
            if (context != null)
            {
                this.FormQuestionViewModel = context as FormQuestionViewModel;
            }
        }
    }
}
