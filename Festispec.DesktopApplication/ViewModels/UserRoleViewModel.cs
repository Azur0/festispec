using GalaSoft.MvvmLight;
using SharedCode.Models;

namespace Festispec.DesktopApplication.ViewModels
{
    public class UserRoleViewModel  : ViewModelBase
    {
        private UserRole _role;

        public UserRoleViewModel(UserRole role)
        {
            this._role = role;
        }

        public string Role
        {
            get => this._role.Role;
            set { this._role.Role = value; }
        }
    }
}
