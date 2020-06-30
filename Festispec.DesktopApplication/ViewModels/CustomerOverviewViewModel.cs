using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class CustomerOverviewViewModel : ViewModelBase
    {
        #region Model References

        private List<CustomerViewModel> _customers;

        #endregion

        #region Getters & Setters

        public int Size
        {
            get => this._customers.Count();
        }

        #endregion

        public void GetAll()
        {
            UnitOfWork uow = new UnitOfWork();
            uow.CustomerRepository
                .Get()
                .ToList()
                .ForEach(x => this._customers.Add(new CustomerViewModel(x)));

            uow.Dispose();
        }

        //TODO: Call on currently selected?
        public void Delete(CustomerViewModel toBeDeleted)
        {
            UnitOfWork uow = new UnitOfWork();
            uow.CustomerRepository.Delete(toBeDeleted.ToModel());
            uow.Save();
            uow.Dispose();
        }
    }
}
