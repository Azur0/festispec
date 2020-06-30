using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Quotation : ISoftDeletable
    {
        public int Id { get; private set; }
        public int EventId { get; set; }
        public double Price { get; set; }
        public string QuotationStatusStatus { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual QuotationStatus QuotationStatusStatusNavigation { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
