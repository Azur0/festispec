using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Customer : ISoftDeletable
    {
        public Customer()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; private set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Kvk { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Event> Event { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
