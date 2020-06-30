using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Report : ISoftDeletable
    {
        public Report()
        {
            ReportHasEvents = new HashSet<ReportHasEvents>();
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public int FileLinkId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual FileLink FileLink { get; set; }
        public virtual ICollection<ReportHasEvents> ReportHasEvents { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
