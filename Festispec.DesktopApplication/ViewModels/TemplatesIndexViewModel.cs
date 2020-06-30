using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class TemplatesIndexViewModel : ViewModelBase
    {
        public List<TemplateViewModel> Templates { get; set; }

        public void LoadAllTemplates()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.Templates = unitOfWork.InspectionFormRepository
                    .Get()
                    .Where(form => form.DeletedAt == null)
                    .Where(form => form.EventInspectionId == null)
                    .Select(form => new TemplateViewModel(form))
                    .ToList();
            }
        }

        public void DeleteUserByPK(int pk)
        {
            // remove from list
            this.Templates = Templates
                .Where(u => u.Id != pk)
                .ToList();


            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                InspectionForm templateToDelete = unitOfWork.InspectionFormRepository
                    .Get(u => u.Id == pk)
                    .First();
                
                unitOfWork.InspectionFormRepository.Delete(templateToDelete);
            }
        }
    }
}
