using System;
using Windows.UI.Popups;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public static class MessageBox
    {
        public async static void Show(string message, string title = "Info")
        {
            await (new MessageDialog(message ?? "STRING WAS NULL", title)).ShowAsync();
        }
    }
}
