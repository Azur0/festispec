using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.DesktopControllers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;
using Festispec.DesktopApplication.Views.Components;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Festispec.DesktopApplication.Views
{

    public sealed partial class UsersCreate : Page
    {
        public UserViewModel UserViewModel { get; set; }

        DropDownItems dropDownItems { get; set; }

        public UsersCreate()
        {
            this.InitializeComponent();

            this.UserViewModel = new UserViewModel();
            this.dropDownItems = new DropDownItems();

            this.DoAsync(() =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var roles = unitOfWork.UserRoleRepository.Get();

                foreach (var role in roles)
                {
                    this.MainUI(() =>
                    {
                        dropDownItems.AddItem(role.Role, role.Role.Substring(0, 1).ToUpper() + role.Role.Substring(1, role.Role.Length - 1).ToLower());
                    });
                }
            });
        }

        private void SaveUser(object sender, TappedRoutedEventArgs e)
        {
            // force set all field values
            var customFieldComponents = this.GetChildOfType<UserControl>(this);
            bool anyHasError = false;
            foreach (var field in customFieldComponents)
            {
                field.ForceSetValue();
                if (field.HasError == true)
                {
                    anyHasError = true;
                }
            }

            //if (anyHasError) return;

            this.saveToDatabase();
        }

        private void saveToDatabase()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var tempUser = new User();
                tempUser.UserRole = "admin";
                tempUser.FirstName = "Bader";
                tempUser.LastName = "Ali";
                tempUser.Email = "BaderAliRaja@Live.nl";
                tempUser.Password = "test1234";
                var tempLocatie = new Location();
                tempLocatie.Postalcode = "1234AB";
                tempLocatie.Number = "100";
                tempLocatie.City = "Boxtel";
                tempLocatie.Longtitude = 10;
                tempLocatie.Latitude = 11;
                tempUser.Location = tempLocatie;

                var results = unitOfWork.LocationRepository.Get(l => l.Postalcode == tempLocatie.Postalcode && l.Number == tempLocatie.Number && l.City == tempLocatie.City);
                
                if (results.Count() > 0)
                {
                    tempUser.Location = results.First();
                }
                
                unitOfWork.UserRepository.Insert(tempUser);
                unitOfWork.Save();
                unitOfWork.Dispose();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Database Fout");
            }
            
        }

        private void DoAsync(Action action)
        {
            overlay.Visibility = Visibility.Visible;

            Task.Run(() =>
            {
                action.Invoke();

                this.MainUI(() =>
                {
                    overlay.Visibility = Visibility.Collapsed;
                });
            });
        }

        private async void MainUI(Action action)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => action());
        }

        private IEnumerable<IComponent> GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
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

        
    }
}
