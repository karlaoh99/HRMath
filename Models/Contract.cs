using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Contract
    {
        public Guid IdP { get; set; }
        public int IdF { get; set; }
        public DateTime StartDate { get; set; }
        public string Role { get; set; }
        public string Type { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Faculty IdFNavigation { get; set; }
        public virtual Professor IdPNavigation { get; set; }
    }
}
