using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Festispec.DesktopApplication.Views.Components;
using Windows.UI.Core;
using System.Net.NetworkInformation;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public static class PageHelper
    {
        public static void DoAsync(UserControl overlay, CoreDispatcher dispatcher, Action action)
        {
            overlay.Visibility = Visibility.Visible;

            Task.Run(() =>
            {
                action.Invoke();

                PageHelper.MainUI(dispatcher, () =>
                {
                    overlay.Visibility = Visibility.Collapsed;
                });
            });
        }

        public static async void MainUI(CoreDispatcher dispatcher, Action action)
        {
            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => action());
        }

        public static IEnumerable<IComponent> GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as IComponent);

                if (result == null)
                {
                    var newlist = GetChildOfType<T>(child);
                    foreach (IComponent nestedResult in newlist) yield return nestedResult;
                }
                else yield return result;
            }
            yield break;
        }

        private static bool? _isOnline { get; set; }
        public static bool IsOnline()
        {
            if (_isOnline == null)
            {
                _isOnline = new Ping().Send("www.google.com").Status == IPStatus.Success;
            }

            return _isOnline.Value;
        }
    }
}
