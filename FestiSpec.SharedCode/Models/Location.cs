using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Location : ISoftDeletable
    {
        public Location()
        {
            Customer = new HashSet<Customer>();
            Event = new HashSet<Event>();
            Law = new HashSet<Law>();
            User = new HashSet<User>();
        }

        public int Id { get; private set; }
        public string Postalcode { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public double? Longtitude { get; set; }
        public double? Latitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Law> Law { get; set; }
        public virtual ICollection<User> User { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
