using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
	public sealed partial class DateTimeField : UserControl, IComponent, IComponentWidth
    {
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public bool HasError { get; set; }
        public int ComponentWidth { get; set; } = 600;

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(DateTimeField), new PropertyMetadata(default(DateTime)));


        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set
            {
                try
                {
                    SetValue(DateProperty, value);
                    Bindings.Update();
                }
                catch (Exception e)
                {
                    this.ShowError(e.Message);
                }
            }
        }

        public DateTimeField()
        {
            this.InitializeComponent();
            RegisterPropertyChangedCallback(DateProperty, (e, o) => { this.HideError(); });
        }

        public void ShowError(string message)
        {
            this.lblError.Text = message;
            this.datePicker.Style = Application.Current.Resources["FormInputError"] as Style;
            this.HasError = true;
        }

        public void HideError()
        {
            this.lblError.Text = "";
            this.datePicker.Style = Application.Current.Resources["FormInput"] as Style;
            this.HasError = false;
        }

        public void ForceSetValue()
        {
            
        }
    }

    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new DateTimeOffset(((DateTime)value).ToUniversalTime());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((DateTimeOffset)value).DateTime;
        }
    }
}
