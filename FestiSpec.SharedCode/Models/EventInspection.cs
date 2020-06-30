using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class EventInspection : ISoftDeletable
    {
        public EventInspection()
        {
            InspectionForm = new HashSet<InspectionForm>();
        }

        public int Id { get; private set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime ExecutionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual ICollection<InspectionForm> InspectionForm { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
