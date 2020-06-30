using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            User = new HashSet<User>();
        }

        public string Role { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
