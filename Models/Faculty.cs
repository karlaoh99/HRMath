using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Contract = new HashSet<Contract>();
            Grade = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<Grade> Grade { get; set; }
    }
}
