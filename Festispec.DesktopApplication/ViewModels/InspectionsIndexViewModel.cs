using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class InspectionsIndexViewModel : ViewModelBase
    {
        public List<InspectionViewModel> inspections { get; set; }

        public void LoadAllInspections()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.inspections = unitOfWork.EventInspectionRepository
                    .GetIncludes("Event")
                    .Where(inspection => inspection.DeletedAt == null)
                    .Select(inspection => new InspectionViewModel(inspection))
                    .ToList();
            }
        }

        public void DeleteInspectionByPK(int pk)
        {
            // remove from list
            this.inspections = inspections.ToList().Where(u => u.Id != pk).ToList();

            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var inspectionToDelete = unitOfWork.EventInspectionRepository.Get(u => u.Id == pk).First();
                unitOfWork.EventInspectionRepository.Delete(inspectionToDelete);
                unitOfWork.Save();
            }
        }
    }
}
