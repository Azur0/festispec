using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication.Views.Components
{
	class BtnPrimary : Button
	{
		public bool disabled
		{
			get; set;
		}
		public BtnPrimary()
		{
			this.Style = Application.Current.Resources["BtnPrimary"] as Style;
			disabled = false;
		}

		public new void PointerEntered()
		{
			this.Style = Application.Current.Resources["BtnPrimaryHover"] as Style;
		}

		public new void PointerExited()
		{
			this.Style = Application.Current.Resources["BtnPrimary"] as Style;
		}
	}
}
