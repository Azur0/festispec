using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class User : ISoftDeletable
    {
        public User()
        {
            Answer = new HashSet<Answer>();
            Assignees = new HashSet<Assignees>();
            UserHasAvailability = new HashSet<UserHasAvailability>();
        }

        public int Id { get; set; }
        public string UserRole { get; set; }
        public int? LocationId { get; set; }
        public byte[] Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Location Location { get; set; }
        public virtual UserRole UserRoleNavigation { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Assignees> Assignees { get; set; }
        public virtual ICollection<UserHasAvailability> UserHasAvailability { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
