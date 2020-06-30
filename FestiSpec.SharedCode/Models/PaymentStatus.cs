using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            Event = new HashSet<Event>();
        }

        public string PaymentStatus1 { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
