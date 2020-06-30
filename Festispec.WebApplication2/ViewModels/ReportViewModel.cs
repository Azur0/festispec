using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharedCode.Models;

namespace Festispec.WebApplication2.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public List<InspectionForm> EventForms { get; set; }
    }
}