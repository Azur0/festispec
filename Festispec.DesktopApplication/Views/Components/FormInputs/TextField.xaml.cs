using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{

    public partial class TextField : UserControl, IComponent, IComponentWidth
	{
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public bool HasError { get; set; }
		public int ComponentWidth { get; set; } = 600;

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextField), new PropertyMetadata(default(string)));        


        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set {
                try
                {
                    SetValue(TextProperty, value);
                    Bindings.Update();
                }
                catch(Exception e)
                {
                    this.ShowError(e.Message);
				}
                
            }
        }

		public TextField()
        {
            this.InitializeComponent();
            RegisterPropertyChangedCallback(TextProperty, (e, o) => { this.HideError(); });
		}


        public void ShowError(string message)
        {
            this.lblError.Text = message;
			this.textBox.Style = Application.Current.Resources["FormInputError"] as Style;
			this.HasError = true;
		}

        public void HideError()
        {
            this.lblError.Text = "";
			this.textBox.Style = Application.Current.Resources["FormInput"] as Style;
			this.HasError = false;
		}

        public void ForceSetValue()
        {
            this.Text = this.textBox.Text + " ";
        }
    }
}
