using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
    public partial class DropDown : UserControl, IComponent
    {
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public ObservableCollection<Tuple<object, string>> Options { get; set; }
        public bool HasError { get; set; }

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(Tuple<object, string>), typeof(DropDown), new PropertyMetadata(null));
		public int ComponentWidth { get; set; } = 600;
		private Tuple<object, string> selectedValue { get; set; }
        public Action<Tuple<object, string>> onChangeEvent { get; set; }

        public Tuple<object, string> SelectedValue
        {
            get => this.selectedValue;
            set
            {
                if (value == null) return;
                if (this.Options == null) return;
                if (value.Item1 == (selectedValue == null ? null : selectedValue.Item1)) return;

                this.selectedValue = this.Options.Where(x => x.Item1.ToString() == value.Item1.ToString()).First();

                SetValue(SelectedValueProperty, this.selectedValue);
                Bindings.Update();
            }
        }

        public DropDown()
        {
            this.InitializeComponent();
            RegisterPropertyChangedCallback(SelectedValueProperty, (e, o) => { this.HideError(); });
        }

        public void ShowError(string message)
        {
            this.lblError.Text = message;
            this.HasError = true;
			this.comboBox.Style = Application.Current.Resources["ComboBoxError"] as Style;
		}

        public void HideError()
        {
            this.lblError.Text = "";
            this.HasError = false;
			this.comboBox.Style = Application.Current.Resources["ComboBox"] as Style;
		}

        public void ForceSetValue()
        {
            if (comboBox.SelectedValue == null)
            {
                this.ShowError("Kies een optie");
            }

            this.SelectedValue = (Tuple<object,string>) comboBox.SelectedValue;
        }

        public void Reset()
        {
            this.selectedValue = null;
            Bindings.Update();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            onChangeEvent?.Invoke(this.SelectedValue);
        }
    }
}
