using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Festispec.WebApplication.ViewModels
{
    public class InspectionFormViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public List<FormQuestion> FormQuestion { get; set; }
    }
}
