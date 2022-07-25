using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class TeachSubject
    {
        public Guid IdP { get; set; }
        public int IdS { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public string Modality { get; set; }

        public virtual Professor IdPNavigation { get; set; }
        public virtual Subject IdSNavigation { get; set; }
    }
}
