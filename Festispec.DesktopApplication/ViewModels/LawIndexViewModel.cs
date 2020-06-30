using FestiSpec.SharedCode.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class LawIndexViewModel
    {
        public List<LawViewModel> laws { get; set; }

        public void LoadAllLaws()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.laws = unitOfWork.LawRepository.GetIncludes("Location")
                    .Where(l => l.DeletedAt == null)
                    .Select(l => new LawViewModel(l))
                    .ToList();
            }
        }

        public void DeleteLawByPK(int pk)
        {
            // remove from list
            this.laws = laws.ToList().Where(u => u.Id != pk).ToList();

            // remove from database
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var lawToDelete = unitOfWork.LawRepository.Get(l => l.Id == pk).First();
                unitOfWork.LawRepository.Delete(lawToDelete);
                unitOfWork.Save();
            }
        }
    }
}

