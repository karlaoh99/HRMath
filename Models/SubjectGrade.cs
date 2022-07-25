using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class SubjectGrade
    {
        public int IdS { get; set; }
        public int IdG { get; set; }

        public virtual Grade IdGNavigation { get; set; }
        public virtual Subject IdSNavigation { get; set; }
    }
}
