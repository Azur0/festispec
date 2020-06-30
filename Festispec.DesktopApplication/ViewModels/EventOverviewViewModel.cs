using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class EventOverviewViewModel : ViewModelBase
    {
        public List<EventViewModel> Events { get; set; }

        public void LoadAllUsers()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            this.Events = unitOfWork.EventRepository
                .GetIncludes("Customer.Location")
                .Where(e => e.DeletedAt == null)
                .Select(e => new EventViewModel(e))
                .ToList();

            unitOfWork.Dispose();
        }


        public void DeleteEventByPK(int pk)
        {
            // remove from list
            this.Events = Events.ToList().Where(e => e.Id != pk).ToList();

            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var eventToDelete = unitOfWork.EventRepository
                    .Get(u => u.Id == pk)
                    .First();
                
                unitOfWork.EventRepository.Delete(eventToDelete);
                unitOfWork.Save();
            }
        }
    }
}