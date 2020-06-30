using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class UsersIndexViewModel : ViewModelBase
    {
        public List<UserViewModel> Users { get; set; }

        public void LoadAllUsers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.Users = unitOfWork.UserRepository
                    .GetIncludes("Location")
                    .Where(u => u.DeletedAt == null)
                    .Select(u => new UserViewModel(u))
                    .ToList();
            }
        }

        public void DeleteUserByPK(int pk)
        {
            // remove from list
            this.Users = Users.ToList().Where(u => u.Id != pk).ToList();

            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                User userToDelete = unitOfWork.UserRepository
                    .Get(u => u.Id == pk)
                    .First();
                
                unitOfWork.UserRepository.Delete(userToDelete);
                unitOfWork.Save();
            }
        }
    }
}
