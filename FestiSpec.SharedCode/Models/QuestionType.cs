using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            FormQuestion = new HashSet<FormQuestion>();
        }

        public string QuestionType1 { get; set; }

        public virtual ICollection<FormQuestion> FormQuestion { get; set; }
    }
}
