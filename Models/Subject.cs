using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Class = new HashSet<Class>();
            SubjectGrade = new HashSet<SubjectGrade>();
            TeachSubject = new HashSet<TeachSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAnual { get; set; }
        public bool IsOptative { get; set; }

        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<SubjectGrade> SubjectGrade { get; set; }
        public virtual ICollection<TeachSubject> TeachSubject { get; set; }
    }
}
