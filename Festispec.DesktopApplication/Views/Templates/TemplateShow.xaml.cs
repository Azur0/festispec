using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TemplateShow : Page
	{
		public TemplateViewModel TemplateViewModel;

		public TemplateShow()
		{
			this.InitializeComponent();
			TemplateViewModel = new TemplateViewModel();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			int TemplateId = ( e.Parameter as int? ).Value;

			PageHelper.DoAsync(overlay, Dispatcher, () =>
			{
				TemplateViewModel.LoadTemplateById(TemplateId);

				PageHelper.MainUI(Dispatcher, () =>
				{
					this.Bindings.Update();
				});
			});

		}

		private void EditTemplate(object sender, TappedRoutedEventArgs e)
		{
			Router.GoToPage<TemplateEdit>(this.TemplateViewModel.Id);
		}

		private void CancelClick(object sender, TappedRoutedEventArgs e)
		{
			Router.GoBack();
		}
	}
}
