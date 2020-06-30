using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Data.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Festispec.DesktopApplication.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsersView : Page
    {
        public List<TextFilterDescriptor> filters { get; set; }
        public TextFilterDescriptor nameFilter { get; set; }
        public TextFilterDescriptor emailFilter { get; set; }
        public TextFilterDescriptor addressFilter { get; set; }
        public TextFilterDescriptor roleFilter { get; set; }
        List<DataGridUser> users { get;set; }

        public UsersView()
        {
            this.InitializeComponent();

            SetFilter();

            users = new List<DataGridUser>();

            PageHelper.DoAsync(overlay, Dispatcher, () =>
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var dbUsers = unitOfWork.UserRepository.Get();

                foreach (var dbUser in dbUsers)
                {
                    users.Add(new DataGridUser()
                    {
                        Id = dbUser.Id,
                        Name = $"{dbUser.FirstName} {dbUser.LastName}",
                        Email = dbUser.Email,
                        Adres = "Dont Know",
                        Role = dbUser.UserRole
                    });

                    PageHelper.MainUI(Dispatcher, () =>
                    {
                        this.DataGrid.DataContext = this.users;
                    });
                }

            });
        }

        private void OpenUsersCreatePage(object sender, TappedRoutedEventArgs e)
        {
            Router.GoToPage<UsersCreate>();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                foreach(var filter in filters)
                {
                    DataGrid.FilterDescriptors.Remove(filter);
                }
            }
            else
            {
                foreach(var filter in filters)
                {
                    if (!DataGrid.FilterDescriptors.Contains(filter))
                        DataGrid.FilterDescriptors.Add(filter);
                }
            }

            foreach(var filter in filters)
            {
                filter.Value = ((TextBox)sender).Text;
            }
        }

        private void PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void SetFilter()
        {
            filters = new List<TextFilterDescriptor>() {
                new TextFilterDescriptor() { PropertyName = "Name", IsCaseSensitive = false, Operator = TextOperator.Contains},
            };
        }

        private void OpenUsersViewPage(object sender, TappedRoutedEventArgs e)
        {
            var userId = ((Button)sender).Tag;
            Router.GoToPage<UsersShow>(userId);
        }

        private void DeleteUser(object sender, TappedRoutedEventArgs e)
        {
            int userId = (((Button)sender).Tag as int?).Value;
            this.users = users.ToList().Where(u => u.Id != userId).ToList();
            UnitOfWork unitOfWork = new UnitOfWork();
            var userToDelete = unitOfWork.UserRepository.Get(u => u.Id == userId).First();
            unitOfWork.UserRepository.Delete(userToDelete);
            this.DataGrid.DataContext = this.users;
        }

        private void OpenUsersEditPage(object sender, TappedRoutedEventArgs e)
        {
            int userId = (((Button)sender).Tag as int?).Value;
            Router.GoToPage<UsersEdit>(userId);
        }
    }

    struct DataGridUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Role { get; set; }
    }
}
