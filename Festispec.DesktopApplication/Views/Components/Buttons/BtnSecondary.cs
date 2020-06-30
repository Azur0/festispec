using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication.Views.Components
{
	class BtnSecondary : Button
	{
		public BtnSecondary()
		{
			this.Style = Application.Current.Resources["BtnSecondary"] as Style;
		}
	}
}
