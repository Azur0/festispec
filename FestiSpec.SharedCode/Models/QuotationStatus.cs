using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class QuotationStatus
    {
        public QuotationStatus()
        {
            Quotation = new HashSet<Quotation>();
        }

        public string Status { get; set; }

        public virtual ICollection<Quotation> Quotation { get; set; }
    }
}
