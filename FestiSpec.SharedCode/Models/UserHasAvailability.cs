using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class UserHasAvailability
    {
        public int UserId { get; set; }
        public DateTime Day { get; set; }

        public virtual User User { get; set; }
    }
}
