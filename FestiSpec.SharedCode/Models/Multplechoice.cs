using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class Multplechoice
    {
        public int Id { get; private set; }
        public string Option { get; set; }
        public int FormQuestionId { get; set; }

        public virtual FormQuestion FormQuestion { get; set; }
    }
}
