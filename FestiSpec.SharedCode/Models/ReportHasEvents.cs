using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class ReportHasEvents
    {
        public int ReportId { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Report Report { get; set; }
    }
}
