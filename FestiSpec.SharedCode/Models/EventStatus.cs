using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class EventStatus
    {
        public EventStatus()
        {
            Event = new HashSet<Event>();
        }

        public string Status { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
