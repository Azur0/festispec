using Festispec.DesktopApplication.DesktopControllers;
using Festispec.DesktopApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
	public sealed partial class LoginView : Page
	{
        private LoginViewModel loginViewModel = new LoginViewModel();

        public LoginView()
		{
			this.InitializeComponent();

            if (NetworkController.HasInternet() == false)
            {
                PageHelper.MainUI(Dispatcher, () =>
                {
                    Router.User = new User() { FirstName = "OFFLINE" };
                    Router.RootGoTo<MainPage>();
                });
                return;
            }
        }

        private void Login(object sender, TappedRoutedEventArgs e)
        {
            var email = emailBox.Text;
            var password = passwordBox.Password;

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                try
                {
                    Validators.Check(email, "Email", "NotEmpty|Email");
                    Validators.Check(password, "Wachtwoord", "NotEmpty");

                    var user = loginViewModel.LoginUser(email, password);

                    if (user == null)
                    {
                        PageHelper.MainUI(Dispatcher, () =>
                        {
                            errorBox.ShowError("Email en/of wachtwoord combinatie komt niet voor.");
                        });

                        return;
                    }

                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        Router.User = user;
                        Router.RootGoTo<MainPage>();
                    });
                }
                catch (Exception error)
                {

                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        errorBox.ShowError(error.Message);
                    });

                }

            });
            
        }

    }
}
