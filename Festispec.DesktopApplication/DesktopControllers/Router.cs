
﻿using SharedCode.Models;
using System;
using Windows.UI.Xaml;

using Windows.UI.Xaml.Controls;

namespace Festispec.DesktopApplication
{
    public static class Router
    {
        private static Frame Frame;

        public static User User;

        public static void SetPage(ref Frame frame) => Frame = frame;

        public static void RootGoTo<T>()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(T));
        }

        public static void GoToPage<T>(object parameter = null)
        {
            Frame.Navigate(typeof(T), parameter);
        }

        public static void GoBack()
        {
            Frame.GoBack();

        }
    }
}
