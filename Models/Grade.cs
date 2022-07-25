using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Grade
    {
        public Grade()
        {
            SubjectGrade = new HashSet<SubjectGrade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdF { get; set; }

        public virtual Faculty IdFNavigation { get; set; }
        public virtual ICollection<SubjectGrade> SubjectGrade { get; set; }
    }
}
