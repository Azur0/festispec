using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        public User LoginUser(string email, string password)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UserRepository.Get(u => u.Email == email && u.Password == Crypt.SHA256(password)).FirstOrDefault();
            }
        }

    }
}
