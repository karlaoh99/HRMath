using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Contract = new HashSet<Contract>();
            TeachClass = new HashSet<TeachClass>();
            TeachSubject = new HashSet<TeachSubject>();
        }

        public Guid Id { get; set; }
        public string PersonalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Landphone { get; set; }
        public string Address { get; set; }
        public string ScientificGrade { get; set; }
        public string TeachingCategory { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<TeachClass> TeachClass { get; set; }
        public virtual ICollection<TeachSubject> TeachSubject { get; set; }
    }
}
