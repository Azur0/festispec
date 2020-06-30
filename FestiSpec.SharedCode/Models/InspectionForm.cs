using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class InspectionForm : ISoftDeletable
    {
        public InspectionForm()
        {
            Assignees = new HashSet<Assignees>();
            FormQuestion = new HashSet<FormQuestion>();
        }

        public int Id { get; private set; }
        public int? EventInspectionId { get; set; }
        public string Name { get; set; }
        public string FloorPlan { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual EventInspection EventInspection { get; set; }
        public virtual ICollection<Assignees> Assignees { get; set; }
        public virtual ICollection<FormQuestion> FormQuestion { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
