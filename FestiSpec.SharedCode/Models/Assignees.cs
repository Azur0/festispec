using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Assignees
    {
        public int UserId { get; set; }
        public int InspectionFormId { get; set; }
        public bool? Completed { get; set; }

        public virtual InspectionForm InspectionForm { get; set; }
        public virtual User User { get; set; }
    }
}
