using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication.Views.Components
{
	class BtnBar : RelativePanel
	{
		public BtnBar()
		{
			this.Style = Application.Current.Resources["BtnBarRelative"] as Style;
		}
	}
}
