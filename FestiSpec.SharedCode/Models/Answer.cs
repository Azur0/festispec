using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Answer
    {
        public int Id { get; private set; }
        public int UserId { get; set; }
        public int FormQuestionId { get; set; }
        public string Value { get; set; }

        public virtual FormQuestion FormQuestion { get; set; }
        public virtual User User { get; set; }
    }
}
