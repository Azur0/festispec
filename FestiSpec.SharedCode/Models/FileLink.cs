using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedCode.Models
{
    public partial class FileLink : ISoftDeletable
    {
        public FileLink()
        {
            Report = new HashSet<Report>();
        }

        public int Id { get; private set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Report> Report { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
