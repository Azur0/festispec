using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication.Views.Components
{
	public sealed partial class TextArea : UserControl, IComponent
	{
		public string Title { get; set; }
		public string Placeholder { get; set; }
		public bool HasError { get; set; }
		public int ComponentWidth { get; set; } = 600;

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextField), new PropertyMetadata(default(string)));

		public TextArea()
		{
			this.InitializeComponent();
			RegisterPropertyChangedCallback(TextProperty, (e, o) => { this.HideError(); });
		}

		public string Text
		{
			get
			{
				return (string)GetValue(TextProperty);
			}
			set
			{
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

		public void ShowError(string message)
		{
			this.lblError.Text = message;
			this.textArea.Style = Application.Current.Resources["FormInputError"] as Style;
			this.HasError = true;
		}

		public void HideError()
		{
			this.lblError.Text = "";
			this.textArea.Style = Application.Current.Resources["FormInput"] as Style;
			this.HasError = false;
		}

		public void ForceSetValue()
		{
			this.Text = this.textArea.Text + " ";
		}
	}
}
