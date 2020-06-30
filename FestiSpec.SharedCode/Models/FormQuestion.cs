using FestiSpec.SharedCode.Repositories;
using System;
using System.Collections.Generic;

namespace SharedCode.Models
{
    public partial class FormQuestion : ISoftDeletable
    {
        public FormQuestion()
        {
            Answer = new HashSet<Answer>();
            Multplechoice = new HashSet<Multplechoice>();
        }

        public int Id { get; private set; }
        public int InspectionId { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public string Instructions { get; set; }
        public string Question { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual InspectionForm Inspection { get; set; }
        public virtual QuestionType TypeNavigation { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Multplechoice> Multplechoice { get; set; }

        public void Delete()
        {
            DeletedAt = DateTime.Now;
        }
    }
}
