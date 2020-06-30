using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class CustomersIndexViewModel : ViewModelBase
    {
        public List<CustomerViewModel> customers { get; set; }

        public void LoadAllCustomers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.customers = unitOfWork.CustomerRepository
                    .GetIncludes("Location")
                    .Where(c => c.DeletedAt == null)
                    .Select(c => new CustomerViewModel(c))
                    .ToList();
            }
        }

        public void DeleteCustomerByPK(int pk)
        {
            // remove from list
            this.customers = customers.ToList().Where(u => u.Id != pk).ToList();

            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var customerToDelete = unitOfWork.CustomerRepository.Get(c => c.Id == pk).First();
                unitOfWork.CustomerRepository.Delete(customerToDelete);
                unitOfWork.Save();
            }
        }
    }
}
