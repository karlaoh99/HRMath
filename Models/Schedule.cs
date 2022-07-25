using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Class = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}
