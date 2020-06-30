using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Festispec.WebApplication.ViewModels
{
    public class DashBoardViewModel
    {
        private int userid;

        public DashBoardViewModel(object v)
        {
            userid = (int)v;
        }

        public IEnumerable<Assignees> FetchAssignedInspections()
        {
            IEnumerable<Assignees> assignees;

            using (UnitOfWork uow = new UnitOfWork())
            { 
                assignees = uow.AssigneesRepository.GetIncludes("InspectionForm.EventInspection.Event.Location", u => u.UserId == userid && u.Completed == null);
            }

            return assignees;
        }
    }
}
