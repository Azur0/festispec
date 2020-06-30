
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication.Views.Components
{
	class BtnRound : Button
	{
		public BtnRound()
		{
			this.Style = Application.Current.Resources["BtnRound"] as Style;
		}
	}
}
