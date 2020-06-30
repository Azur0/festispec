using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Event : ISoftDeletable
    {
        public Event()
        {
            EventInspection = new HashSet<EventInspection>();
            Quotation = new HashSet<Quotation>();
            ReportHasEvents = new HashSet<ReportHasEvents>();
        }

        public int Id { get; private set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public string EventStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string About { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual EventStatus EventStatusNavigation { get; set; }
        public virtual Location Location { get; set; }
        public virtual PaymentStatus PaymentStatusNavigation { get; set; }
        public virtual ICollection<EventInspection> EventInspection { get; set; }
        public virtual ICollection<Quotation> Quotation { get; set; }
        public virtual ICollection<ReportHasEvents> ReportHasEvents { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
