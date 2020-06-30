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
	
	public sealed partial class GroupWrapper : UserControl
	{
		public string Title { get; set; } = "PlaceholderText";
		public int ComponenWidth { get; set; } = 400;

		private int _componentHeight { get; set; }

        public int ComponentHeight
		{
            get
            {
                return _componentHeight;
            }
            set
            {
                _componentHeight = value;
                if (value != 0)
                {
                    this.ContentArea.Height = value;
                }
            }
        }

		public object ChildContent
		{
			get
			{
				return this.wrapperContent.Content;
			}
			set
			{
				this.wrapperContent.Content = value;
			}
		}
		public object WrapperBtn
		{
			get
			{
				return this.WrapperHeaderBtn.Content;
			}
			set
			{
				this.WrapperHeaderBtn.Content = value;
			}
		}

		public GroupWrapper()
		{
			this.InitializeComponent();
        }
	}
}
