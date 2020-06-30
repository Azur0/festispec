using Festispec.DesktopApplication.DesktopControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Festispec.DesktopApplication.Views.Components
{
	public sealed partial class FillableList : UserControl, IComponent
    {
		public string Title { get; set; }

        public List<string> SelectedOptions { get;set; }

        public static readonly DependencyProperty SelectedOptionsProperty = DependencyProperty.Register("SelectedOptions", typeof(List<string>), typeof(FillableList), new PropertyMetadata(null));

        public bool HasError { get; set; }

		public FillableList()
		{
            this.InitializeComponent();
            listView.ItemsSource = SelectedOptions;
            this.SelectedOptions = new List<string>();
        }

        private void AddNewItemClick(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // validate
            if (textBox.Text == "") return;

            if (SelectedOptions.Contains(textBox.Text))
            {
                this.ShowError("Opties moeten uniek zijn.");
                return;
            }

            SelectedOptions.Add(textBox.Text);
            this.HideError();
            SetValue(SelectedOptionsProperty, this.SelectedOptions);
			textBox.Text = "";
            this.RefreshList();
        }

        private void RemoveItemClick(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            string text = ((Button)sender).Tag.ToString();
            SelectedOptions = SelectedOptions.Where(x => x != text).ToList();
            SetValue(SelectedOptionsProperty, this.SelectedOptions);
            this.RefreshList();
        }

        private void RefreshList()
        {
            listView.ItemsSource = null; 
            listView.ItemsSource = SelectedOptions;
        }

        public void ShowError(string message)
        {
            this.lblError.Text = message;
            // TODO: Apply red style
            this.HasError = true;
        }

        public void HideError()
        {
            this.lblError.Text = "";
            // TODO: Remove red style
            this.HasError = false;
        }

        public void Reset()
        {
            if (this.SelectedOptions == null) return;
            this.SelectedOptions.Clear();
            RefreshList();
        }

        public void SetOptions(List<string> list)
        {
            this.SelectedOptions = list;
            RefreshList();
        }

        public void ForceSetValue()
        {
            HideError();
            if (this.SelectedOptions.Count < 2)
            {
                this.ShowError("Je moet minimaal 2 opties toevoegen.");
            }
        }
    }
}
